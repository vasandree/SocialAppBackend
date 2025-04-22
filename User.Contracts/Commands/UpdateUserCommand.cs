using MediatR;
using User.Contracts.Dtos.Requests;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Contracts.Commands;

public record UpdateUserCommand(Guid UserId, SocialNetwork SocialNetwork, InitDataDto InitData)
    : IRequest<ApplicationUser>;