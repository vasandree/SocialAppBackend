using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.Auth.CreateTokens;
using UserService.Application.Features.Commands.User.AddUser;
using UserService.Application.Features.Commands.User.UpdateUser;
using UserService.Application.Helpers.TelegramHelper;
using UserService.Persistence.Repositories.TelegramAccountRepository;

namespace UserService.Application.Features.Commands.Auth.LoginWithTelegram;

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

        Domain.Entities.User user;

        if (await _telegramAccountRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
        {
            var userByTelegramIdAsync =
                await _telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);
            user = await _mediator.Send(
                new UpdateUserCommand(userByTelegramIdAsync.Id, Domain.Enums.SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            //todo: update telegram account
        }
        else
        {
            user = await _mediator.Send(new AddUserCommand(Domain.Enums.SocialNetwork.Telegram, request.InitData),
                cancellationToken);
            //todo: add telegram account to user
        }

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}