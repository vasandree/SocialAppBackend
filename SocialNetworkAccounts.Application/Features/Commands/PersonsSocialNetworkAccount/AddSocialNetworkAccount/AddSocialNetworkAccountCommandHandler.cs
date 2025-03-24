using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.AddSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler: IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public AddSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _personsAccountRepository = personsAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }

    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {

        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");
        
        //todo: check person existence
        
        if (await _personsAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.SocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.SocialNetworkAccountDto.Type} account already added");


        await _personsAccountRepository.AddAsync(new PersonsAccount()
        {
            CreatorId = request.UserId,
            Username = request.SocialNetworkAccountDto.Username,
            Type = request.SocialNetworkAccountDto.Type,
            PersonsId = request.PersonId
        });

        return Unit.Value;
    }
}