using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.User.UpdateUser;

public record UpdateUserCommand(Guid UserId, Domain.Enums.SocialNetwork SocialNetwork, InitDataDto InitData) : IRequest<Domain.Entities.User>;