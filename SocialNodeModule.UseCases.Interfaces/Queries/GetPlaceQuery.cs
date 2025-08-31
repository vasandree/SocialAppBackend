using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetPlaceQuery(Guid PlaceId, Guid UserId) : IRequest<PlaceDto>;