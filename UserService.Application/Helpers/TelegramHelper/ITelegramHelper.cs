using UserService.Application.Dtos.Telegram;
using UserService.Domain.Enums;

namespace UserService.Application.Helpers.TelegramHelper;

public interface ITelegramHelper
{
    TelegramInitData ParseInitData(string data);
    bool ValidateInitData(string data);
    Language GetLanguage(string code);
}