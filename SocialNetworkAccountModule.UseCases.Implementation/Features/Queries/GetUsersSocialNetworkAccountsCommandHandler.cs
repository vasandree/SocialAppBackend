using AutoMapper;
using MediatR;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.UseCases.Interfaces.Queries;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Queries;

internal sealed class
    GetUsersSocialNetworkAccountsCommandHandler(
        IMapper mapper,
        IUsersAccountRepository usersAccountRepository)
    : IRequestHandler<GetUsersSocialNetworkAccountsQuery,
        List<SocialNetworkAccountDto>>
{
    public async Task<List<SocialNetworkAccountDto>> Handle(GetUsersSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accounts = await usersAccountRepository.GetAllByUserId(request.UserId);

        return mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}