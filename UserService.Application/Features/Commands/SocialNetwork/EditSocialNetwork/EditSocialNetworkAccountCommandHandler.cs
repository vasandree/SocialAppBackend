using Common.Exceptions;
using MediatR;
using UserService.Persistence.Repositories.SocialNetworkAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.SocialNetwork.EditSocialNetwork;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly ISocialNetworkAccountRepository _socialNetworkAccountRepository;

    public EditSocialNetworkAccountCommandHandler(IUserRepository userRepository,
        ISocialNetworkAccountRepository socialNetworkAccountRepository)
    {
        _userRepository = userRepository;
        _socialNetworkAccountRepository = socialNetworkAccountRepository;
    }

    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _socialNetworkAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        if (!await _userRepository.CheckIfUserExistsByIdAsync(request.UserId))
            throw new BadRequest("User does not exist");

        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        var account = await _socialNetworkAccountRepository.GetById(request.SocialNetworkAccountId);

        if (user!.Id != account!.UserId) throw new Forbidden("Provided account does not belong to you");

        account.Username = request.SocialNetworkAccountDto.Username;
        account.Url = request.SocialNetworkAccountDto.Url;

        await _socialNetworkAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}