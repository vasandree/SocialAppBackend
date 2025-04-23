using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Responses;

namespace SocialNetworkAccounts.Contracts.Queries;

public record GetPersonsSocialNetworkAccountsQuery(Guid UserId, Guid PersonId): IRequest<List<SocialNetworkAccountDto>>;