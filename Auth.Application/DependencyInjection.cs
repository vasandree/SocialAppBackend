using System.Reflection;
using Auth.Application.BackgroundTasks;
using Auth.Application.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using User.Contracts.Helpers;

namespace Auth.Application;

public static class DependencyInjection
{
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddScoped<IJwtService, JwtService>();
        
        builder.Services.AddQuartz(q =>
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

        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}