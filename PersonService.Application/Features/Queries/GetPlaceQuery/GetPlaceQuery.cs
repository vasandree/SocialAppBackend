using MediatR;
using PersonService.Application.Dtos.Responses;
using PersonService.Application.Dtos.Responses.Place;

namespace PersonService.Application.Features.Queries.GetPlaceQuery;

public record GetPlaceQuery(Guid PlaceId, Guid UserId) : IRequest<PlaceDto>;