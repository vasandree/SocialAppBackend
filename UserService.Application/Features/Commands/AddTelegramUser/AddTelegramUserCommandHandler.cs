using MediatR;
using UserService.Application.Helpers.TelegramHelper;
using UserService.Domain.Entities;
using UserService.Persistence.Repositories.TelegramAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.AddTelegramUser;

public class AddTelegramUserCommandHandler : IRequestHandler<AddTelegramUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly ITelegramHelper _telegramHelper;
    private readonly ITelegramAccountRepository _telegramAccountRepository;

    public AddTelegramUserCommandHandler(IUserRepository userRepository, ITelegramHelper telegramHelper,
        ITelegramAccountRepository telegramAccountRepository)
    {
        _userRepository = userRepository;
        _telegramHelper = telegramHelper;
        _telegramAccountRepository = telegramAccountRepository;
    }

    public async Task<User> Handle(AddTelegramUserCommand request, CancellationToken cancellationToken)
    {
        var parsedTelegramData = _telegramHelper.ParseInitData(request.InitData.InitData);

        var user = new User
        {
            FirstName = parsedTelegramData.User.First_Name,
            LastName = parsedTelegramData.User.Last_Name,
            UserName = parsedTelegramData.User.Username,
            PhotoUrl = parsedTelegramData.User.Photo_Url,
            Language = _telegramHelper.GetLanguage(parsedTelegramData.User.Language_Code),
        };

        var telegramAccount = new TelegramAccount
        {
            Id = parsedTelegramData.User.Id,
            Userneme = parsedTelegramData.User.Username,
            FirstName = parsedTelegramData.User.First_Name,
            LastName = parsedTelegramData.User.Last_Name,
            LanguageCode = parsedTelegramData.User.Language_Code,
            AllowsWriteToPm = parsedTelegramData.User.Allows_Write_To_Pm,
            PhotoUrl = parsedTelegramData.User.Photo_Url,
            User = user,
            UserId = user.Id,
        };

        user.TelegramAccount = telegramAccount;

        await _userRepository.AddAsync(user);
        await _telegramAccountRepository.AddAsync(telegramAccount);

        return user;
    }
}