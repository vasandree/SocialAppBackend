using AutoMapper;
using Common.Exceptions;
using MediatR;
using PersonService.Application.Dtos.Responses.Person;
using PersonService.Persistence.Repositories.PersonRepository;

namespace PersonService.Application.Features.Queries.GetPersonQuery;

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        if (!await _personRepository.CheckIfExists(request.PersonId))
            throw new NotFound("Provided person not found");

        if (!await _personRepository.CheckIfUserIsCreator(request.UserId, request.PersonId))
            throw new Forbidden("You are not allowed to get this person");

        var person = await _personRepository.GetByIdAsync(request.PersonId);
        return _mapper.Map<PersonDto>(person);
    }
}