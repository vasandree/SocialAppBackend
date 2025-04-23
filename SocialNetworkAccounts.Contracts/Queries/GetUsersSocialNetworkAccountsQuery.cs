using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Responses;

namespace SocialNetworkAccounts.Contracts.Queries;

public record GetUsersSocialNetworkAccountsQuery(Guid UserId) : IRequest<List<SocialNetworkAccountDto>>;