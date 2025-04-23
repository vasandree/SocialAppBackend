using AutoMapper;
using MediatR;
using SocialNetworkAccounts.Contracts.Dtos.Responses;
using SocialNetworkAccounts.Contracts.Queries;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Queries;

public class
    GetUsersSocialNetworkAccountsCommandHandler : IRequestHandler<GetUsersSocialNetworkAccountsQuery,
    List<SocialNetworkAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IUsersAccountRepository _usersAccountRepository;

    public GetUsersSocialNetworkAccountsCommandHandler(IMapper mapper, IUsersAccountRepository usersAccountRepository)
    {
        _mapper = mapper;
        _usersAccountRepository = usersAccountRepository;
    }


    public async Task<List<SocialNetworkAccountDto>> Handle(GetUsersSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        
        //todo: check user existsnce

        var accounts = await _usersAccountRepository.GetAllByUserId(request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}