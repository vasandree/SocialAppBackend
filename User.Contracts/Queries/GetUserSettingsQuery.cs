using MediatR;
using User.Contracts.Dtos;

namespace User.Contracts.Queries;

public record GetUserSettingsQuery(Guid UserId) : IRequest<UserSettingsDto>;