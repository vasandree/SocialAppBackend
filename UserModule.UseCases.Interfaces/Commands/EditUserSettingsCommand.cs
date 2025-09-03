using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace UserModule.UseCases.Interfaces.Commands;

public record EditUserSettingsCommand(Guid UserId, UserSettingsDto Settings) : IRequest<UserSettingsDto>;