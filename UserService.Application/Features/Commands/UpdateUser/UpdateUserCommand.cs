using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, SocialNetwork SocialNetwork, InitDataDto InitData) : IRequest<User>;