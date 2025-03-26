using MassTransit;
using Microsoft.AspNetCore.Builder;
using UserService.Infrastructure.Consumers;

namespace UserService.Infrastructure;

public static class UserServiceInfrastructureConfiguration
{
    public static void AddUserServiceInfrastructureConfiguration(this WebApplicationBuilder builder)
    {
        
        var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ");

        var host = rabbitMqSettings["Host"] ?? throw new ArgumentNullException("RabbitMQ Host is not configured.");
        var username = rabbitMqSettings["Username"] ?? throw new ArgumentNullException("RabbitMQ Username is not configured.");
        var password = rabbitMqSettings["Password"] ?? throw new ArgumentNullException("RabbitMQ Password is not configured.");

        
        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumer<CheckUserExistenceConsumer>();
            
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
                
                cfg.ReceiveEndpoint("check-user-existence", e =>
                {
                    e.Consumer<CheckUserExistenceConsumer>(context);
                });
            });
        });
    }
}