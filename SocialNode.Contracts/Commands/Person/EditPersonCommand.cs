using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.Person;

public record EditPersonCommand(Guid UserId, Guid PersonId, PersonRequestDto PersonRequestDto) : IRequest<Unit>;