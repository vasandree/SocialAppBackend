using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace NotificationModule.BackgroundJobs;

public static class DependencyInjection
{
    public static void AddNotificationModuleBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var outboxJobKey = new JobKey("OutboxNotificationJob");
            q.AddJob<OutboxNotificationJob>(opts => opts.WithIdentity(outboxJobKey));
            q.AddTrigger(opts => opts
                .ForJob(outboxJobKey)
                .WithIdentity("OutboxNotificationJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever()));
            
            var deleteJobKey = new JobKey("DeleteDeliveredMessagesJob");
            q.AddJob<DeleteDeliveredMessagesJob>(opts => opts.WithIdentity(deleteJobKey));
            q.AddTrigger(opts => opts
                .ForJob(deleteJobKey)
                .WithIdentity("DeleteDeliveredMessagesJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(1)
                    .RepeatForever()));
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}
