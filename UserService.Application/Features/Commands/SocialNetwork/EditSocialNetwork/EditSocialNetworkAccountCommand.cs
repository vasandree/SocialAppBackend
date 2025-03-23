using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.SocialNetwork.EditSocialNetwork;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid SocialNetworkAccountId,
    EditSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;