using MediatR;

namespace PersonService.Application.Features.Commands.Persons.DeletePerson;

public record DeletePersonCommand(Guid UserId, Guid PersonId) : IRequest<Unit>;