using UserModule.Domain.Enums;

namespace AuthModule.UseCases.Interfaces.Dtos.Responses;

public record UserSettingsDto(Theme Theme, Language LanguageCode);