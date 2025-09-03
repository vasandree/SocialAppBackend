namespace AuthModule.UseCases.Interfaces.Dtos.Responses;

public record AuthResponse(TokensDto Tokens, UserSettingsDto UserSettings);
