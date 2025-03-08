using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.AddUser;

public record AddUserCommand(SocialNetwork SocialNetwork, InitDataDto InitData): IRequest<User>;