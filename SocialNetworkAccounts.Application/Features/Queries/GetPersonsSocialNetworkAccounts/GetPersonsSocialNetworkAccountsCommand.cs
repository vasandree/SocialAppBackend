using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;

namespace SocialNetworkAccounts.Application.Features.Queries.GetPersonsSocialNetworkAccounts;

public record GetPersonsSocialNetworkAccountsCommand(Guid UserId, Guid PersonId): IRequest<List<SocialNetworkAccountDto>>;