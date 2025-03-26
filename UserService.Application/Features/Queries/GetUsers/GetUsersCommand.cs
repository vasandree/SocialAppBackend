using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Queries.GetUsers;

public record GetUsersCommand(Guid UserId, string? SearchTerm = null) : IRequest<List<ShortenUserDto>>;
