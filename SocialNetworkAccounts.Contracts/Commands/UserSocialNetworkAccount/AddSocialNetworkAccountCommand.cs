using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, AddSocialNetworkAccountDto AddSocialNetworkAccountDto)
    : IRequest<Unit>;