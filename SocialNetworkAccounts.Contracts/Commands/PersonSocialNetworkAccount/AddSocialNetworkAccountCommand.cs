using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(
    Guid UserId,
    Guid PersonId,
    AddSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;