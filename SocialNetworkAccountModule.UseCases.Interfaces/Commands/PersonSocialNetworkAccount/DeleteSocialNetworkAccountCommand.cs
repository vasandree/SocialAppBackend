using MediatR;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;

public record DeleteSocialNetworkAccountCommand(Guid UserId, Guid AccountId): IRequest<Unit>;