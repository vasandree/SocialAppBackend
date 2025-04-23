using MediatR;
using SocialNode.Contracts.Dtos.Responses.Place;

namespace SocialNode.Contracts.Queries;

public record GetPlaceQuery(Guid PlaceId, Guid UserId) : IRequest<PlaceDto>;