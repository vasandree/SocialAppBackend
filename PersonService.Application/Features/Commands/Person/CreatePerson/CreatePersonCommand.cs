using MediatR;
using PersonService.Application.Dtos.Requests;

namespace PersonService.Application.Features.Commands.Person.CreatePerson;

public record CreatePersonCommand(Guid UserId, PersonRequestDto PersonRequestDto) : IRequest<Unit>;