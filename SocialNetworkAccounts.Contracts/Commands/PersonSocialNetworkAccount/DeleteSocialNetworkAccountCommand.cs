using MediatR;

namespace SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid AccountId): IRequest<Unit>;