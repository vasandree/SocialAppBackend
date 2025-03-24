namespace Common.ServiceBus;

using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public abstract class BaseConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
{
    private readonly ILogger<BaseConsumer<TMessage>> _logger;

    protected BaseConsumer(ILogger<BaseConsumer<TMessage>> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        try
        {

            await HandleMessageAsync(context);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing message of type {MessageType} with payload: {@Payload}", typeof(TMessage), context.Message);
            throw;
        }
    }

    protected abstract Task HandleMessageAsync(ConsumeContext<TMessage> context);
}
