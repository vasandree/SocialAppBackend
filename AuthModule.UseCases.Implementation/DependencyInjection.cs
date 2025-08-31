using System.Reflection;
using AuthModule.UseCases.Implementation.BackgroundTasks;
using AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;
using AuthModule.UseCases.Implementation.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using UserModule.UseCases.Interfaces.Helpers;

namespace AuthModule.UseCases.Implementation;

public static class DependencyInjection
{
    public static void AddAuthModuleUseCases(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ILoginCommandFactory, LoginFactory>();

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