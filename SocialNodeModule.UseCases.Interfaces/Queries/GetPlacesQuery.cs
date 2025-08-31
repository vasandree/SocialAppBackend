using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetPlacesQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPlacesDto>;