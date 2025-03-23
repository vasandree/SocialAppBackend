using Common.Exceptions;
using MediatR;
using UserService.Domain.Entities;
using UserService.Persistence.Repositories.SocialNetworkAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.SocialNetwork.AddSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly ISocialNetworkAccountRepository _socialNetworkAccountRepository;
    private readonly IUserRepository _userRepository;

    public AddSocialNetworkAccountCommandHandler(ISocialNetworkAccountRepository socialNetworkAccountRepository,
        IUserRepository userRepository)
    {
        _socialNetworkAccountRepository = socialNetworkAccountRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (await _socialNetworkAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");

        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (user == null) throw new BadRequest("User does not exist");

        await _socialNetworkAccountRepository.AddAsync(new SocialNetworkAccount
        {
            UserId = request.UserId,
            Username = request.AddSocialNetworkAccountDto.Username,
            Url = request.AddSocialNetworkAccountDto.Url,
            Type = request.AddSocialNetworkAccountDto.Type,
            User = user
        });

        return Unit.Value;
    }
}