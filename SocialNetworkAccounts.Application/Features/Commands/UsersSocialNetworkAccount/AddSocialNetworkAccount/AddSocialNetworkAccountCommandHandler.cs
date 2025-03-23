using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.AddSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;

    public AddSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository)
    {
        _usersAccountRepository = usersAccountRepository;
    }


    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _usersAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");

        //todo: check user existence

        await _usersAccountRepository.AddAsync(new UsersAccount()
        {
            UserId = request.UserId,
            Username = request.AddSocialNetworkAccountDto.Username,
            Type = request.AddSocialNetworkAccountDto.Type,
        });

        return Unit.Value;
    }
}