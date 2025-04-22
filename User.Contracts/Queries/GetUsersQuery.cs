using MediatR;
using User.Contracts.Dtos.Responses;

namespace User.Contracts.Queries;

public record GetUsersQuery(Guid UserId, string? SearchTerm = null) : IRequest<List<ShortenUserDto>>;
