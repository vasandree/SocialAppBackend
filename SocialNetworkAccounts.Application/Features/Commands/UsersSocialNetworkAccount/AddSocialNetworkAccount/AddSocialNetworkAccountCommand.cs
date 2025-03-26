using MediatR;
using SocialNetworkAccounts.Application.Dtos.Requests;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount.AddSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, AddSocialNetworkAccountDto AddSocialNetworkAccountDto)
    : IRequest<Unit>;