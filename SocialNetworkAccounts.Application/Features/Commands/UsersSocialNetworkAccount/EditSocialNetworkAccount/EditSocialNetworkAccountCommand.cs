using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.EditSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid SocialNetworkAccountId,
    EditSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;