using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, AddSocialNetworkAccountDto AddSocialNetworkAccountDto)
    : IRequest<Unit>;