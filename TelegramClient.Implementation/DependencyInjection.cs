using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Shared.Extensions.Configs;
using TelegramClient.Interfaces;

namespace TelegramClient.Implementation;

public static class DependencyInjection
{
     public static void AddNotificationHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<ITelegramClient, TelegramClient>("NotificationSender",
                (sp, client) =>
                {
                    var httpConfig = sp.GetRequiredService<IOptions<NotificationHttpConfig>>().Value;

                    if (!string.IsNullOrWhiteSpace(httpConfig.BaseUrl))
                    {
                        client.BaseAddress = new Uri(httpConfig.BaseUrl);
                    }

                    client.Timeout = TimeSpan.FromSeconds(Math.Max(1, httpConfig.TimeoutSeconds));
                })
            .AddResilienceHandler("notification-pipeline", (builder, context) =>
            {
                var resilience = context.ServiceProvider
                    .GetRequiredService<IOptions<NotificationResilienceConfig>>().Value;

                builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    MaxRetryAttempts = resilience.MaxRetryAttempts,
                    Delay = TimeSpan.FromMilliseconds(resilience.RetryDelayMilliseconds),
                    BackoffType = resilience.RetryBackoffType == "Exponential"
                        ? DelayBackoffType.Exponential
                        : DelayBackoffType.Constant,
                    UseJitter = resilience.RetryUseJitter,
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
                    FailureRatio = resilience.CircuitBreakerFailureRatio,
                    MinimumThroughput = resilience.CircuitBreakerMinimumThroughput,
                    BreakDuration = TimeSpan.FromSeconds(resilience.CircuitBreakerBreakDurationSeconds),
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

                builder.AddTimeout(TimeSpan.FromSeconds(resilience.PollyTimeoutSeconds));
                builder.AddConcurrencyLimiter(permitLimit: resilience.ConcurrencyPermitLimit);
            });
    }
}