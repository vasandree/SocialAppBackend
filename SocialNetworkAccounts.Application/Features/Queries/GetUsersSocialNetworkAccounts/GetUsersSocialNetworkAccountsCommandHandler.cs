using AutoMapper;
using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Queries.GetUsersSocialNetworkAccounts;

public class
    GetUsersSocialNetworkAccountsCommandHandler : IRequestHandler<GetUsersSocialNetworkAccountsCommand,
    List<SocialNetworkAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IUsersAccountRepository _usersAccountRepository;

    public GetUsersSocialNetworkAccountsCommandHandler(IMapper mapper, IUsersAccountRepository usersAccountRepository)
    {
        _mapper = mapper;
        _usersAccountRepository = usersAccountRepository;
    }


    public async Task<List<SocialNetworkAccountDto>> Handle(GetUsersSocialNetworkAccountsCommand request,
        CancellationToken cancellationToken)
    {

        //todo: check user existence
        
        var accounts = await _usersAccountRepository.GetAllByUserId(request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}