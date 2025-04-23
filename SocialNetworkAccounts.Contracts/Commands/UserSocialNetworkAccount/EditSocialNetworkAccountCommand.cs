using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid SocialNetworkAccountId,
    EditSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;