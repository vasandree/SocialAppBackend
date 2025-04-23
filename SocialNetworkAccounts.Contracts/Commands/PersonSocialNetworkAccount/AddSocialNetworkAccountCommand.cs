using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(
    Guid UserId,
    Guid PersonId,
    AddSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;