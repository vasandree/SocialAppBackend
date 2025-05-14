using System.Windows.Input;
using MediatR;

namespace Event.Contracts.Commands.Event;

public record RemoveSocialNodeFromEventCommand(Guid EventId, Guid UserId, Guid NodeId) : IRequest<Unit>;