using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using MediatR;
using Shared.Domain;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Commands;
using User.Contracts.Helpers;
using User.Contracts.Repositories;
using User.Domain.Entities;

namespace Auth.Application.Features.Commands;

public class LoginWithTelegramCommandHandler : IRequestHandler<LoginWithTelegramCommand, TokensDto>
{
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly ISender _mediator;

    public LoginWithTelegramCommandHandler(ITelegramHelper telegramHelper,
        ISender mediator, ITelegramAccountRepository telegramAccountRepository,
        IUsersAccountRepository usersAccountRepository)
    {
        _telegramHelper = telegramHelper;
        _mediator = mediator;
        _telegramAccountRepository = telegramAccountRepository;
        _usersAccountRepository = usersAccountRepository;
    }

    public async Task<TokensDto> Handle(LoginWithTelegramCommand request, CancellationToken cancellationToken)
    {
        if (request.InitData == null)
            throw new BadRequest("No InitData provided");

        if (!_telegramHelper.ValidateInitData(request.InitData.InitData))
            throw new Unauthorized("Failed to authenticate");

        var parsedInitData = _telegramHelper.ParseInitData(request.InitData.InitData);

        if (parsedInitData.User == null) throw new BadRequest("Invalid InitData");

        ApplicationUser? user;

        if (await _telegramAccountRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
        {
            var userByTelegramIdAsync =
                await _telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);
            user = await _mediator.Send(
                new UpdateUserCommand(userByTelegramIdAsync.Id, SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            var socialNetworkAccount =
                await _usersAccountRepository.GetByUserIdAndTypeAsync(user.Id, SocialNetwork.Telegram);
            if (socialNetworkAccount != null)
                await _mediator.Send(new EditSocialNetworkAccountCommand(user.Id, socialNetworkAccount.Id,
                    new EditSocialNetworkAccountDto
                    {
                        Username = user.UserName
                    }), cancellationToken);
        }
        else
        {
            user = await _mediator.Send(new AddUserCommand(SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            await _mediator.Send(new AddSocialNetworkAccountCommand(user.Id, new AddSocialNetworkAccountDto
            {
                Type = SocialNetwork.Telegram,
                Username = user.UserName
            }), cancellationToken);
        }

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}