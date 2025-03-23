using MediatR;

namespace UserService.Application.Features.Commands.SocialNetwork.DeleteSocialNetwork;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid SocialNetworkAccountId): IRequest<Unit>;