using System.Reflection;
using Auth.Application.BackgroundTasks;
using Auth.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using User.Contracts.Helpers;

namespace Auth.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IJwtService, JwtService>();

        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("CleanupExpiredTokensJob");

            q.AddJob<CleanupExpiredTokensJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("CleanupExpiredTokensTrigger")
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInHours(1)
                    .RepeatForever()));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}