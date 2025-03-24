using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.EditSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public EditSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _personsAccountRepository = personsAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }

    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");

        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");

        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.EditSocialNetworkAccountDto.Username;

        await _personsAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}