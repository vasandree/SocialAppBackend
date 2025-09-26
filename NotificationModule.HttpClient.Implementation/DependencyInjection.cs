using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationModule.HttpClient.Interfaces;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Shared.Extensions.Configs;

namespace NotificationModule.HttpClient.Implementation;

public static class DependencyInjection
{
    public static void AddNotificationHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<INotificationSenderClient, NotificationSenderClient>("NotificationSender",
                (sp, client) =>
                {
                    var cfg = sp.GetRequiredService<IOptions<NotificationHttpConfig>>().Value;
                    if (!string.IsNullOrWhiteSpace(cfg.BaseUrl))
                    {
                        client.BaseAddress = new Uri(cfg.BaseUrl);
                    }
                    client.Timeout = TimeSpan.FromSeconds(Math.Max(1, cfg.TimeoutSeconds));
                })
            .AddResilienceHandler("notification-pipeline", builder =>
            {
                // 🔹 Retry с экспонентой + джиттер
                builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    MaxRetryAttempts = 5,
                    Delay = TimeSpan.FromMilliseconds(200),
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle = args =>
                    {
                        return ValueTask.FromResult(
                            args.Outcome switch
                            {
                                { Exception: HttpRequestException } => true,
                                { Result.StatusCode: HttpStatusCode.RequestTimeout } => true,
                                { Result.StatusCode: HttpStatusCode.TooManyRequests } => true,
                                { Result.StatusCode: >= HttpStatusCode.InternalServerError } => true,
                                _ => false
                            });
                    }
                });

                builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
                {
                    FailureRatio = 0.5,             // 50% ошибок
                    MinimumThroughput = 10,         // минимум 10 запросов для оценки
                    BreakDuration = TimeSpan.FromSeconds(30), // "остановка" на 30 секунд
                    ShouldHandle = args =>
                    {
                        return ValueTask.FromResult(
                            args.Outcome switch
                            {
                                { Exception: HttpRequestException } => true,
                                { Result.StatusCode: HttpStatusCode.RequestTimeout } => true,
                                { Result.StatusCode: HttpStatusCode.TooManyRequests } => true,
                                { Result.StatusCode: >= HttpStatusCode.InternalServerError } => true,
                                _ => false
                            });
                    }
                });

                builder.AddTimeout(TimeSpan.FromSeconds(10));

                builder.AddConcurrencyLimiter(permitLimit: 5); // максимум 5 одновременных запросов
            });
    }
}