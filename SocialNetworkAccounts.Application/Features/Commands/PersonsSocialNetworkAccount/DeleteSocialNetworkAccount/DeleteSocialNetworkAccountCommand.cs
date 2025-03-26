using MediatR;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.DeleteSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid AccountId): IRequest<Unit>;