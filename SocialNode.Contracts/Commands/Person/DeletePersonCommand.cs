using MediatR;

namespace SocialNode.Contracts.Commands.Person;

public record DeletePersonCommand(Guid UserId, Guid PersonId) : IRequest<Unit>;