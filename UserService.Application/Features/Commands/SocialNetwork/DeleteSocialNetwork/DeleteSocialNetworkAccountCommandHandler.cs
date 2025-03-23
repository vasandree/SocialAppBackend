using Common.Exceptions;
using MediatR;
using UserService.Persistence.Repositories.SocialNetworkAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.SocialNetwork.DeleteSocialNetwork;

public class DeleteSocialNetworkAccountCommandHandler : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly ISocialNetworkAccountRepository _socialNetworkAccountRepository;
    private readonly IUserRepository _userRepository;

    public DeleteSocialNetworkAccountCommandHandler(IUserRepository userRepository,
        ISocialNetworkAccountRepository socialNetworkAccountRepository)
    {
        _userRepository = userRepository;
        _socialNetworkAccountRepository = socialNetworkAccountRepository;
    }

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfUserExistsByIdAsync(request.UserId))
            throw new BadRequest("User does not exist");

        if (!await _socialNetworkAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        var account = await _socialNetworkAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account != null && account.UserId != request.UserId) throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _socialNetworkAccountRepository.DeleteAsync(account);
        
        return Unit.Value;
    }
}