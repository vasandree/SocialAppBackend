using MediatR;

namespace SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid SocialNetworkAccountId): IRequest<Unit>;