using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Queries;

public record GetUsersSocialNetworkAccountsQuery(Guid UserId) : IRequest<List<SocialNetworkAccountDto>>;