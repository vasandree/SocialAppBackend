using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UpdateUser;

public record UpdateUserCommand(SocialNetwork SocialNetwork, InitDataDto InitData) : IRequest<Unit>;