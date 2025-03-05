using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.AddUser;
using UserService.Application.Features.Commands.CreateTokens;
using UserService.Application.Features.Commands.UpdateUser;
using UserService.Application.Helpers.TelegramHelper;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.LoginWithTelegram;

public class LoginWithTelegramCommandHandler : IRequestHandler<LoginWithTelegramCommand, TokensDto>
{
    private readonly ITelegramHelper _telegramHelper;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public LoginWithTelegramCommandHandler(ITelegramHelper telegramHelper, IUserRepository userRepository,
        IMediator mediator)
    {
        _telegramHelper = telegramHelper;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<TokensDto> Handle(LoginWithTelegramCommand request, CancellationToken cancellationToken)
    {
        if (request.InitData == null)
            throw new BadRequest("No InitData provided");

        if (!_telegramHelper.ValidateInitData(request.InitData.InitData))
            throw new Unauthorized("Failed to authenticate");

        var parsedInitData = _telegramHelper.ParseInitData(request.InitData.InitData);

        if (await _userRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
        {
            await _mediator.Send(new UpdateUserCommand(parsedInitData.User), cancellationToken);
        }
        else
        {
            await _mediator.Send(new AddUserCommand(parsedInitData.User), cancellationToken);
        }

        var user = await _userRepository.GetByTelegramIdAsync(parsedInitData.User.Id);

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}