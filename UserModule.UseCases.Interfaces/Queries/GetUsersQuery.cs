using MediatR;
using UserModule.UseCases.Interfaces.Dtos.Responses;

namespace UserModule.UseCases.Interfaces.Queries;

public record GetUsersQuery(Guid UserId, string? SearchTerm = null) : IRequest<List<ShortenUserDto>>;
