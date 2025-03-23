using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Queries.GetSocialNetworkAccounts;

public record GetSocialNetworkAccountsCommand(Guid UserId) : IRequest<List<SocialNetworkAccountDto>>;