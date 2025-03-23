using AutoMapper;
using MediatR;
using SocialNetworkAccounts.Application.Dtos.Responses;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Queries.GetPersonsSocialNetworkAccounts;

public class
    GetPersonsSocialNetworkAccountsCommandHandler : IRequestHandler<GetPersonsSocialNetworkAccountsCommand,
    List<SocialNetworkAccountDto>>
{
    private IMapper _mapper;
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public GetPersonsSocialNetworkAccountsCommandHandler(IPersonsAccountRepository personsAccountRepository, IMapper mapper)
    {
        _personsAccountRepository = personsAccountRepository;
        _mapper = mapper;
    }

    public async Task<List<SocialNetworkAccountDto>> Handle(GetPersonsSocialNetworkAccountsCommand request,
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