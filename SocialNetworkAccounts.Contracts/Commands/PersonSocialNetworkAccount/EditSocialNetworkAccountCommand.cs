using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid AccountId,
    EditSocialNetworkAccountDto EditSocialNetworkAccountDto) : IRequest<Unit>;