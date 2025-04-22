using MediatR;
using User.Contracts.Dtos.Responses;

namespace User.Contracts.Queries;

public record GetUserQuery(Guid UserId): IRequest<UserDto>;