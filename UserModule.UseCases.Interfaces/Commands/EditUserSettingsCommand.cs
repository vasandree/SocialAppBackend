using MediatR;
using UserModule.UseCases.Interfaces.Dtos;

namespace UserModule.UseCases.Interfaces.Commands;

public record EditUserSettingsCommand(Guid UserId, UserSettingsDto Settings) : IRequest<UserSettingsDto>;