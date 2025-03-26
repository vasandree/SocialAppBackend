using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UserService.Application.BackgroundTasks;
using UserService.Application.Helpers.JwtService;
using UserService.Application.Helpers.TelegramHelper;

namespace UserService.Application;

public static class UserServiceApplicationConfiguration
{
    public static void AddUserServiceApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddScoped<ITelegramHelper, TelegramHelper>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        
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