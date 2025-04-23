using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using MediatR;
using Shared.Domain;
using Shared.Domain.Exceptions;
using User.Contracts.Commands;
using User.Contracts.Helpers;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Domain.Enums;

namespace Auth.Application.Features.Commands;

public class LoginWithTelegramCommandHandler : IRequestHandler<LoginWithTelegramCommand, TokensDto>
{
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;
    private readonly ISender _mediator;

    public LoginWithTelegramCommandHandler(ITelegramHelper telegramHelper,
        ISender mediator, ITelegramAccountRepository telegramAccountRepository)
    {
        _telegramHelper = telegramHelper;
        _mediator = mediator;
        _telegramAccountRepository = telegramAccountRepository;
    }

    public async Task<TokensDto> Handle(LoginWithTelegramCommand request, CancellationToken cancellationToken)
    {
        if (request.InitData == null)
            throw new BadRequest("No InitData provided");

        if (!_telegramHelper.ValidateInitData(request.InitData.InitData))
            throw new Unauthorized("Failed to authenticate");

        var parsedInitData = _telegramHelper.ParseInitData(request.InitData.InitData);

        ApplicationUser user;

        if (await _telegramAccountRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
        {
            var userByTelegramIdAsync =
                await _telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);
            user = await _mediator.Send(
                new UpdateUserCommand(userByTelegramIdAsync.Id, SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            //todo: update telegram account
        }
        else
        {
            user = await _mediator.Send(new AddUserCommand(SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            //todo: add telegram account to user
        }

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}