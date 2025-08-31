using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(Guid UserId, AddSocialNetworkAccountDto AddSocialNetworkAccountDto)
    : IRequest<Unit>;