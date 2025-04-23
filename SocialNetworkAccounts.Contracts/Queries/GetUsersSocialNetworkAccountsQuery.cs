using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;

namespace SocialNetworkAccounts.Contracts.Queries;

public record GetUsersSocialNetworkAccountsQuery(Guid UserId) : IRequest<List<SocialNetworkAccountDto>>;