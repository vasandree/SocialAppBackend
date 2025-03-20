using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Configurations;

public static class LoggingConfiguration
{
    public static void AddLoggingConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole(); 
            logging.AddDebug();    
        });
    }
}