using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.EditSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid AccountId,
    EditSocialNetworkAccountDto EditSocialNetworkAccountDto) : IRequest<Unit>;