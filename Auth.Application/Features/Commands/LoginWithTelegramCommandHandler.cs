using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using AutoMapper;
using MediatR;
using Shared.Domain;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Dtos.Requests;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Commands;
using User.Contracts.Dtos;
using User.Contracts.Helpers;
using User.Contracts.Repositories;
using User.Domain.Entities;

namespace Auth.Application.Features.Commands;

public class LoginWithTelegramCommandHandler : IRequestHandler<LoginWithTelegramCommand, AuthResponse>
{
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly ISender _mediator;
    private readonly IUserSettingsRepository _userSettingsRepository;
    private readonly IMapper _mapper;

    public LoginWithTelegramCommandHandler(ITelegramHelper telegramHelper,
        ISender mediator, ITelegramAccountRepository telegramAccountRepository,
        IUsersAccountRepository usersAccountRepository, IUserSettingsRepository userSettingsRepository, IMapper mapper)
    {
        _telegramHelper = telegramHelper;
        _mediator = mediator;
        _telegramAccountRepository = telegramAccountRepository;
        _usersAccountRepository = usersAccountRepository;
        _userSettingsRepository = userSettingsRepository;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(LoginWithTelegramCommand request, CancellationToken cancellationToken)
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

        return new AuthResponse
        {
            Tokens = await _mediator.Send(new CreateTokensCommand(user), cancellationToken),
            Settings = _mapper.Map<UserSettingsDto>(await _userSettingsRepository.GetByUserIdAsync(user.Id))
        };
    }
}