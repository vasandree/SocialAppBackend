using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Person;

public record EditPersonCommand(Guid UserId, Guid PersonId, PersonRequestDto PersonRequestDto) : IRequest<Unit>;