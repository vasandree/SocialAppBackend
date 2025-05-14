using Event.Contracts.Dtos.Responses;
using MediatR;

namespace Event.Contracts.Queries;

public record GetEventQuery(Guid UserId, Guid EventId) : IRequest<EventDto>;