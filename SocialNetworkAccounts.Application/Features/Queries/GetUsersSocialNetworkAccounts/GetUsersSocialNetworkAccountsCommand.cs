using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;

namespace SocialNetworkAccounts.Application.Features.Queries.GetUsersSocialNetworkAccounts;

public record GetUsersSocialNetworkAccountsCommand(Guid UserId) : IRequest<List<SocialNetworkAccountDto>>;