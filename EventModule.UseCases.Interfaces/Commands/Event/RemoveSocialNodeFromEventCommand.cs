using MediatR;

namespace EventModule.UseCases.Interfaces.Commands.Event;

public record RemoveSocialNodeFromEventCommand(Guid EventId, Guid UserId, Guid NodeId) : IRequest<Unit>;