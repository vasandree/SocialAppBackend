using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid SocialNetworkAccountId,
    EditSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;