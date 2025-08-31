using EventModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace EventModule.UseCases.Interfaces.Queries;

public record GetEventQuery(Guid UserId, Guid EventId) : IRequest<EventDto>;