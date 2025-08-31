using MediatR;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Person;

public record DeletePersonCommand(Guid UserId, Guid PersonId) : IRequest<Unit>;