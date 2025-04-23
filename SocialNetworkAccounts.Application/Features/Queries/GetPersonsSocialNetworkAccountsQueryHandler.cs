using AutoMapper;
using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;
using SocialNetworkAccounts.Application.Features.Queries.GetPersonsSocialNetworkAccounts;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Queries;

public class
    GetPersonsSocialNetworkAccountsQueryHandler : IRequestHandler<GetPersonsSocialNetworkAccountsQuery,
    List<SocialNetworkAccountDto>>
{
    private IMapper _mapper;
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public GetPersonsSocialNetworkAccountsQueryHandler(IPersonsAccountRepository personsAccountRepository,
        IMapper mapper)
    {
        _personsAccountRepository = personsAccountRepository;
        _mapper = mapper;
    }

    public async Task<List<SocialNetworkAccountDto>> Handle(GetPersonsSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        //todo: check user existence

        //todo: check person existence

        var accounts =
            await _personsAccountRepository.FindAsync(x =>
                x.PersonsId == request.PersonId && x.CreatorId == request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}