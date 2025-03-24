using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.EditSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public EditSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _usersAccountRepository = usersAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }


    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");
        
        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");
        
        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account.UserId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.SocialNetworkAccountDto.Username;

        await _usersAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}