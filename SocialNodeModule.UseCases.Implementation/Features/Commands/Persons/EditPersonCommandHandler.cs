using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;
using SocialNodeModule.UseCases.Interfaces.Services;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Persons;

internal sealed class EditPersonCommandHandler(
    IPersonRepository personRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<EditPersonCommand, Unit>
{
    public async Task<Unit> Handle(EditPersonCommand request, CancellationToken cancellationToken)
    {
        if (!await personRepository.CheckIfExists(request.PersonId))
            throw new NotFound($"Person with id={request.PersonId} not found");

        var person = await personRepository.GetByIdAsync(request.PersonId);

        if (!person.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to delete");

        var avatar = request.PersonRequestDto.Avatar != null
            ? await cloudStorageService.UploadFileAsync(request.PersonRequestDto.Avatar, person.Id)
            : null;

        person.EditInfo(request.PersonRequestDto.Name, request.PersonRequestDto.Description, avatar,
            request.PersonRequestDto.Email, request.PersonRequestDto.PhoneNumber);

        await personRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}