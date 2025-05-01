using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Dtos.Responses;
using SocialNetworkAccounts.Contracts.Queries;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNode.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Queries;

public class
    GetPersonsSocialNetworkAccountsQueryHandler : IRequestHandler<GetPersonsSocialNetworkAccountsQuery,
    List<SocialNetworkAccountDto>>
{
    private IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public GetPersonsSocialNetworkAccountsQueryHandler(IPersonsAccountRepository personsAccountRepository,
        IMapper mapper, IUserRepository userRepository, IPersonRepository personRepository)
    {
        _personsAccountRepository = personsAccountRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public async Task<List<SocialNetworkAccountDto>> Handle(GetPersonsSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        if (!await _personRepository.CheckIfExists(request.PersonId))
            throw new NotFound("Person does not exist");

        var accounts =
            await _personsAccountRepository.FindAsync(x =>
                x.PersonsId == request.PersonId && x.CreatorId == request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}