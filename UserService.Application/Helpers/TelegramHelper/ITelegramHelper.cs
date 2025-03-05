using UserService.Application.Dtos.Telegram;

namespace UserService.Application.Helpers.TelegramHelper;

public interface ITelegramHelper
{
    TelegramInitData ParseInitData(string data);
    bool ValidateInitData(string data);
}