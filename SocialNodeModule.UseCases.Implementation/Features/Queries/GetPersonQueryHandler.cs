using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetPersonQueryHandler(IPersonRepository personRepository, IMapper mapper)
    : IRequestHandler<GetPersonQuery, PersonDto>
{
    public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        if (!await personRepository.CheckIfExists(request.PersonId))
            throw new NotFound("Provided person not found");

        if (!await personRepository.CheckIfUserIsCreator(request.UserId, request.PersonId))
            throw new Forbidden("You are not allowed to get this person");

        var person = await personRepository.GetByIdAsync(request.PersonId);
        return mapper.Map<PersonDto>(person);
    }
}