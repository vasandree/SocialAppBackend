using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;

public record AddSocialNetworkAccountCommand(
    Guid UserId,
    Guid PersonId,
    AddSocialNetworkAccountDto SocialNetworkAccountDto) : IRequest<Unit>;