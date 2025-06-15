using MediatR;
using User.Contracts.Dtos;

namespace User.Contracts.Commands;

public record EditUserSettingsCommand(Guid UserId, UserSettingsDto Settings) : IRequest<UserSettingsDto>;