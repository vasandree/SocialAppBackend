using MediatR;

namespace PersonService.Application.Features.Commands.Person.DeletePerson;

public record DeletePersonCommand(Guid UserId, Guid PersonId) : IRequest<Unit>;