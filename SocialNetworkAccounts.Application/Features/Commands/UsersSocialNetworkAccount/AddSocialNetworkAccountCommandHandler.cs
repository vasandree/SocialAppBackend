using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IUserRepository _userRepository;

    public AddSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository, IUserRepository userRepository)
    {
        _usersAccountRepository = usersAccountRepository;
        _userRepository = userRepository;
    }


    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new BadRequest("User does not exist");
        
        if (await _usersAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");

        await _usersAccountRepository.AddAsync(new UsersAccount()
        {
            UserId = request.UserId,
            Username = request.AddSocialNetworkAccountDto.Username,
            Type = request.AddSocialNetworkAccountDto.Type,
        });

        return Unit.Value;
    }
}