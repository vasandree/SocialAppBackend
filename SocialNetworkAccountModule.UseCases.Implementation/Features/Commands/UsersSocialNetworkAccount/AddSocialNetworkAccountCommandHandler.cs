using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Commands.UsersSocialNetworkAccount;

internal sealed class AddSocialNetworkAccountCommandHandler(
    IUsersAccountRepository usersAccountRepository)
    : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (await usersAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");

        await usersAccountRepository.AddAsync(new UsersAccount(request.UserId,request.AddSocialNetworkAccountDto.Username,request.AddSocialNetworkAccountDto.Type));

        return Unit.Value;
    }
}