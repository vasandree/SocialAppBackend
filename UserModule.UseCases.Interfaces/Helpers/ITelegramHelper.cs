using UserModule.Domain.Enums;
using UserModule.UseCases.Interfaces.Dtos.Telegram;

namespace UserModule.UseCases.Interfaces.Helpers;

public interface ITelegramHelper
{
    TelegramInitData ParseInitData(string data);
    bool ValidateInitData(string data);
    Language GetLanguage(string? code);
}