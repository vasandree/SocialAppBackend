using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.UseCases.Interfaces.Queries;
using SocialNodeModule.DataAccess.Interfaces.Repositories;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Queries;

internal sealed class
    GetPersonsSocialNetworkAccountsQueryHandler(
        IPersonsAccountRepository personsAccountRepository,
        IMapper mapper,
        IPersonRepository personRepository)
    : IRequestHandler<GetPersonsSocialNetworkAccountsQuery,
        List<SocialNetworkAccountDto>>
{
    public async Task<List<SocialNetworkAccountDto>> Handle(GetPersonsSocialNetworkAccountsQuery request,
        CancellationToken cancellationToken)
    {
        if (!await personRepository.CheckIfExists(request.PersonId))
            throw new NotFound("Person does not exist");

        var accounts =
            await personsAccountRepository.FindAsync(x =>
                x.PersonsId == request.PersonId && x.CreatorId == request.UserId);

        return mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}