using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Common.ServiceBus;


public abstract class BaseRpcSender
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BaseRpcSender> _logger;

    protected BaseRpcSender(IServiceProvider serviceProvider, ILogger<BaseRpcSender> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected async Task<TResponse?> SendRequestAsync<TRequest, TResponse>(TRequest request)
        where TRequest : class
        where TResponse : class
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<TRequest>>();

            _logger.LogInformation("Sending RPC request of type {RequestType}: {@Request}", typeof(TRequest), request);

            var response = await requestClient.GetResponse<TResponse>(request);

            _logger.LogInformation("Received RPC response of type {ResponseType}: {@Response}", typeof(TResponse), response.Message);
            return response.Message;
        }
        catch (RequestTimeoutException)
        {
            _logger.LogError("RPC request timeout for type {RequestType}", typeof(TRequest));
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending RPC request of type {RequestType}", typeof(TRequest));
            throw;
        }
    }
}
