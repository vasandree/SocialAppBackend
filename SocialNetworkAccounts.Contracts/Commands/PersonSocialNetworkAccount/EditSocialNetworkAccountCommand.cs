using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid AccountId,
    EditSocialNetworkAccountDto EditSocialNetworkAccountDto) : IRequest<Unit>;