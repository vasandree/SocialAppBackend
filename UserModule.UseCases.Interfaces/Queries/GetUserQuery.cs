using MediatR;
using UserModule.UseCases.Interfaces.Dtos.Responses;

namespace UserModule.UseCases.Interfaces.Queries;

public record GetUserQuery(Guid UserId): IRequest<UserDto>;