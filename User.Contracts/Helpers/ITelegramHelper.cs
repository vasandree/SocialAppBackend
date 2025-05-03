using User.Contracts.Dtos.Telegram;
using User.Domain.Enums;

namespace User.Contracts.Helpers;

public interface ITelegramHelper
{
    TelegramInitData ParseInitData(string data);
    bool ValidateInitData(string data);
    Language GetLanguage(string? code);
}