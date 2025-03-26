using MediatR;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.DeleteSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid SocialNetworkAccountId): IRequest<Unit>;