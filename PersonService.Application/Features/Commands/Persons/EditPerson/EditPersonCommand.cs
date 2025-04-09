using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Persons.EditPerson;

public record EditPersonCommand(Guid UserId, Guid PersonId, PersonRequestDto PersonRequestDto):IRequest<Unit>;