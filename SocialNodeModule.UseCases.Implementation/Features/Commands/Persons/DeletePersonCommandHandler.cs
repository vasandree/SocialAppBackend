using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Persons;

internal sealed class DeletePersonCommandHandler(IPersonRepository personRepository)
    : IRequestHandler<DeletePersonCommand, Unit>
{
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        if (!await personRepository.CheckIfExists(request.PersonId))
            throw new NotFound($"Person with id={request.PersonId} not found");

        var person = await personRepository.GetByIdAsync(request.PersonId);

        if (!person.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to delete");

        personRepository.DeleteAsync(person);

        await personRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}