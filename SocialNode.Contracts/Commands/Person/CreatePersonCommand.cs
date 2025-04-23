using MediatR;
using SocialNode.Contracts.Dtos.Requests;

namespace SocialNode.Contracts.Commands.Person;

public record CreatePersonCommand(Guid UserId, PersonRequestDto PersonRequestDto) : IRequest<Unit>;