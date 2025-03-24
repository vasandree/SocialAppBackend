using AutoMapper;
using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;
using SocialNetworkAccounts.Infrastructure.ServiceBus.RpcRequestSender;
using SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Queries.GetUsersSocialNetworkAccounts;

public class
    GetUsersSocialNetworkAccountsCommandHandler : IRequestHandler<GetUsersSocialNetworkAccountsCommand,
    List<SocialNetworkAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IRpcRequestSender _rpcRequestSender;
    private readonly IUsersAccountRepository _usersAccountRepository;

    public GetUsersSocialNetworkAccountsCommandHandler(IMapper mapper, IUsersAccountRepository usersAccountRepository, IRpcRequestSender rpcRequestSender)
    {
        _mapper = mapper;
        _usersAccountRepository = usersAccountRepository;
        _rpcRequestSender = rpcRequestSender;
    }


    public async Task<List<SocialNetworkAccountDto>> Handle(GetUsersSocialNetworkAccountsCommand request,
        CancellationToken cancellationToken)
    {

        var userExistence = await _rpcRequestSender.CheckUserExistence(request.UserId);

        if (userExistence is { Exists: true })
            throw new BadRequest("User does not exist");
        
        var accounts = await _usersAccountRepository.GetAllByUserId(request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}