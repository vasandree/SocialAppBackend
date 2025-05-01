using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Dtos.Responses;
using SocialNetworkAccounts.Contracts.Queries;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Queries;

public class
    GetUsersSocialNetworkAccountsCommandHandler : IRequestHandler<GetUsersSocialNetworkAccountsQuery,
    List<SocialNetworkAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUsersAccountRepository _usersAccountRepository;

    public GetUsersSocialNetworkAccountsCommandHandler(IMapper mapper, IUsersAccountRepository usersAccountRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _usersAccountRepository = usersAccountRepository;
        _userRepository = userRepository;
    }


    public async Task<List<SocialNetworkAccountDto>> Handle(GetUsersSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        var accounts = await _usersAccountRepository.GetAllByUserId(request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}