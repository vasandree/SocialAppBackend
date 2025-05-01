using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Dtos.Requests;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNode.Application.Features.Commands.Persons;

public class EditPersonCommandHandler : IRequestHandler<EditPersonCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public EditPersonCommandHandler(IPersonRepository personRepository,
        ICloudStorageService cloudStorageService, IUserRepository userRepository)
    {
        _personRepository = personRepository;
        _cloudStorageService = cloudStorageService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(EditPersonCommand request, CancellationToken cancellationToken)
    {
        if (!await _personRepository.CheckIfExists(request.PersonId))
            throw new NotFound($"Person with id={request.PersonId} not found");

        var person = await _personRepository.GetByIdAsync(request.PersonId);

        if (person!.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete");

        await EditPerson(person, request.PersonRequestDto);

        await _personRepository.UpdateAsync(person);

        return Unit.Value;
    }

    private async Task EditPerson(PersonEntity person, PersonRequestDto newPerson)
    {
        person.Name = newPerson.Name;
        person.Description = newPerson.Description;
        person.Email = newPerson.Email;
        person.PhoneNumber = newPerson.PhoneNumber;
        person.AvatarUrl = newPerson.Avatar != null
            ? await _cloudStorageService.UploadFileAsync(newPerson.Avatar, person.Id)
            : person.AvatarUrl;
    }
}