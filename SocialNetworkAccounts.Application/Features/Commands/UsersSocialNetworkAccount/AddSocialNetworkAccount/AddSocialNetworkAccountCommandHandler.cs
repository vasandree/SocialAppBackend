using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.AddSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IRpcRequestSender _rpcRequestSender;

    public AddSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _usersAccountRepository = usersAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }


    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _usersAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");
        
        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");

        await _usersAccountRepository.AddAsync(new UsersAccount()
        {
            UserId = request.UserId,
            Username = request.AddSocialNetworkAccountDto.Username,
            Type = request.AddSocialNetworkAccountDto.Type,
        });

        return Unit.Value;
    }
}