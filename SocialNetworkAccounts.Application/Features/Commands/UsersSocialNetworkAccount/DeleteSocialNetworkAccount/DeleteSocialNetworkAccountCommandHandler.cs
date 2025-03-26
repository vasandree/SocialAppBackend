using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.DeleteSocialNetworkAccount;

public class DeleteSocialNetworkAccountCommandHandler : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public DeleteSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _usersAccountRepository = usersAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }
    
    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
         var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");

        if (!await _usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account != null && account.UserId != request.UserId)
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _usersAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}