using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;

namespace SocialNetworkAccounts.Application.Features.Queries.GetPersonsSocialNetworkAccounts;

public record GetPersonsSocialNetworkAccountsQuery(Guid UserId, Guid PersonId): IRequest<List<SocialNetworkAccountDto>>;