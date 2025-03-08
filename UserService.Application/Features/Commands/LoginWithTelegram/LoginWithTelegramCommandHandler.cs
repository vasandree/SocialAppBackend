using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.AddUser;
using UserService.Application.Features.Commands.CreateTokens;
using UserService.Application.Features.Commands.UpdateUser;
using UserService.Application.Helpers.TelegramHelper;
using UserService.Domain.Enums;
using UserService.Persistence.Repositories.TelegramAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.LoginCommand.LoginWithTelegram;

public class LoginWithTelegramCommandHandler : IRequestHandler<LoginWithTelegramCommand, TokensDto>
{
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;
    private readonly IMediator _mediator;

    public LoginWithTelegramCommandHandler(ITelegramHelper telegramHelper,
        IMediator mediator, ITelegramAccountRepository telegramAccountRepository)
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

        if (await _telegramAccountRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
        {
            await _mediator.Send(new UpdateUserCommand(SocialNetwork.Telegram, request.InitData), cancellationToken);
        }
        else
        {
            await _mediator.Send(new AddUserCommand(SocialNetwork.Telegram, request.InitData), cancellationToken);
        }

        var user = await _telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}