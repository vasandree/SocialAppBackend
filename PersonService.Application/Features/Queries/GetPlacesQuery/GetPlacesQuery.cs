using MediatR;
using PersonService.Application.Dtos.Responses.Place;

namespace PersonService.Application.Features.Queries.GetPlacesQuery;

public record GetPlacesQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPlacesDto>;