using MediatR;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.AddSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, SocialNetwork SocialNetwork, string Url, string Username)
    : IRequest<Unit>;