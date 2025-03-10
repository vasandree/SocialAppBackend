using MediatR;
using UserService.Application.Helpers.TelegramHelper;
using UserService.Domain.Entities;
using UserService.Persistence.Repositories.TelegramAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.UpdateTelegramUser;

public class UpdateTelegramUserCommandHandler : IRequestHandler<UpdateTelegramUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;

    public UpdateTelegramUserCommandHandler(IUserRepository userRepository,
        ITelegramAccountRepository telegramAccountRepository, ITelegramHelper telegramHelper)
    {
        _userRepository = userRepository;
        _telegramAccountRepository = telegramAccountRepository;
        _telegramHelper = telegramHelper;
    }

    public async Task<User> Handle(UpdateTelegramUserCommand request, CancellationToken cancellationToken)
    {
        var parsedInitData = _telegramHelper.ParseInitData(request.InitData.InitData);

        var user = await _telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);

        user.UserName = parsedInitData.User.Username;
        user.FirstName = parsedInitData.User.First_Name;
        user.LastName = parsedInitData.User.Last_Name;
        user.Language = _telegramHelper.GetLanguage(parsedInitData.User.Language_Code);
        user.PhotoUrl = parsedInitData.User.Photo_Url;

        var telegramAccount = await _telegramAccountRepository.GetByTelegramIdAsync(parsedInitData.User.Id);

        telegramAccount.Userneme = parsedInitData.User.Username;
        telegramAccount.FirstName = parsedInitData.User.First_Name;
        telegramAccount.LastName = parsedInitData.User.Last_Name;
        telegramAccount.PhotoUrl = parsedInitData.User.Photo_Url;
        telegramAccount.LanguageCode = parsedInitData.User.Language_Code;
        telegramAccount.AllowsWriteToPm = parsedInitData.User.Allows_Write_To_Pm;

        await _userRepository.UpdateAsync(user);
        await _telegramAccountRepository.UpdateAsync(telegramAccount);

        return user;
    }
}