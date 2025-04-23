using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid SocialNetworkAccountId,
    EditSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;