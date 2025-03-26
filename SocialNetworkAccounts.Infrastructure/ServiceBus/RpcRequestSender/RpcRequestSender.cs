using Common.ServiceBus;
using Common.ServiceBus.Contracts;
using Microsoft.Extensions.Logging;

namespace SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;

public class RpcRequestSender : BaseRpcSender, IRpcRequestSender
{
    public RpcRequestSender(IServiceProvider serviceProvider, ILogger<BaseRpcSender> logger) : base(serviceProvider,
        logger)
    {
    }

    public async Task<CheckUserExistenceResponse?> CheckUserExistence(Guid userId)
    {
        var request = new CheckUserExistenceRequest(userId);
        return await SendRequestAsync<CheckUserExistenceRequest, CheckUserExistenceResponse>(request);
    }
}