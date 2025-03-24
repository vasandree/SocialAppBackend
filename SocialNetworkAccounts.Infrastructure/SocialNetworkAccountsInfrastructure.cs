using Common.ServiceBus.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;

namespace SocialNetworkAccounts.Infrastructure;

public static class SocialNetworkAccountsInfrastructure
{
    public static void AddSocialNetworkAccountsInfrastructureConfiguration(this WebApplicationBuilder builder)
    {
        
        var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ");

        var host = rabbitMqSettings["Host"] ?? throw new ArgumentNullException("RabbitMQ Host is not configured.");
        var username = rabbitMqSettings["Username"] ?? throw new ArgumentNullException("RabbitMQ Username is not configured.");
        var password = rabbitMqSettings["Password"] ?? throw new ArgumentNullException("RabbitMQ Password is not configured.");
        
        builder.Services.AddMassTransit(config =>
        {
            config.AddRequestClient<CheckUserExistenceRequest>();
            
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
            
            builder.Services.AddScoped<IRpcRequestSender, RpcRequestSender>();
        });
    }
}