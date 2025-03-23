using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.SocialNetwork.AddSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, AddSocialNetworkAccountDto AddSocialNetworkAccountDto)
    : IRequest<Unit>;