using UserModule.UseCases.Interfaces.Dtos;

namespace AuthModule.UseCases.Interfaces.Responses;

public record AuthResponse(TokensDto Tokens, UserSettingsDto UserSettings);
