using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;

public record EditSocialNetworkAccountCommand(
    Guid UserId,
    Guid AccountId,
    EditSocialNetworkAccountDto EditSocialNetworkAccountDto) : IRequest<Unit>;