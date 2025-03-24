using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.DeleteSocialNetworkAccount;

public class DeleteSocialNetworkAccountCommandHandler: IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public DeleteSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _personsAccountRepository = personsAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");

        if (!await _personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");

        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account != null && account.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _personsAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}