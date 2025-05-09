using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.Persons;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository, IUserRepository userRepository)
    {
        _personRepository = personRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        if (!await _personRepository.CheckIfExists(request.PersonId))
            throw new NotFound($"Person with id={request.PersonId} not found");

        var person = await _personRepository.GetByIdAsync(request.PersonId);

        if (person.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");

        await _personRepository.DeleteAsync(person);

        return Unit.Value;
    }
}