using MediatR;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid SocialNetworkAccountId): IRequest<Unit>;