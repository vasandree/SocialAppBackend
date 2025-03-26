using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.User.AddUser;

public record AddUserCommand(Domain.Enums.SocialNetwork SocialNetwork, InitDataDto InitData): IRequest<Domain.Entities.User>;