using Common.ServiceBus.Contracts;

namespace SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;

public interface IRpcRequestSender
{
    Task<CheckUserExistenceResponse?> CheckUserExistence(Guid userId);
}