using MediatR;
using PersonService.Application.Dtos.Responses;

namespace PersonService.Application.Features.Queries.GetPlaceQuery;

public record GetPlaceQuery(Guid PlaceId) : IRequest<PlaceDto>;