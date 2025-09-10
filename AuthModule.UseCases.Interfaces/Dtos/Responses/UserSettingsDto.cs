using UserModule.Domain.Enums;

namespace AuthModule.UseCases.Interfaces.Dtos.Responses;

public record UserSettingsDto
{
    public Theme Theme { get; init; }
    public Language LanguageCode { get; init; }

    public bool TaskReminders { get; init; }
    public bool EventReminders { get; init; }
};