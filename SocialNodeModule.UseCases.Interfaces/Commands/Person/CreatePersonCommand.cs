using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNodeModule.UseCases.Interfaces.Commands.Person;

public record CreatePersonCommand(Guid UserId, PersonRequestDto PersonRequestDto) : IRequest<Unit>;