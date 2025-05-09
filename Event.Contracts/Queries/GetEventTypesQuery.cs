using Event.Contracts.Dtos.Responses;
using MediatR;

namespace Event.Contracts.Queries;

public record GetEventTypesQuery(Guid UserId, string? Name) : IRequest<List<EventTypeResponseDto>>;