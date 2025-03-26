using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.AddSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(
    Guid UserId,
    Guid PersonId,
    AddSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;