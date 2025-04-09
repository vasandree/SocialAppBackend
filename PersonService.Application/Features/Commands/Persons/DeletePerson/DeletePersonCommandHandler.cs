using Common.Exceptions;
using MediatR;
using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.PersonRepository;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Application.Features.Commands.Persons.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        if (!await _personRepository.CheckIfExists(request.PersonId))
            throw new NotFound($"Person with id={request.PersonId} not found");

        var person = await _personRepository.GetByIdAsync(request.PersonId);

        if (person!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");

        await _personRepository.DeleteAsync(person);

        return Unit.Value;
    }
}