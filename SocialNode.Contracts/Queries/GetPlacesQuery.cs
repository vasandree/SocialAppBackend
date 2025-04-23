using MediatR;
using SocialNode.Contracts.Dtos.Responses.Place;

namespace SocialNode.Contracts.Queries;

public record GetPlacesQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPlacesDto>;