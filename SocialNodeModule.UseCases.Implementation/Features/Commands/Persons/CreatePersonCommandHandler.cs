using MediatR;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.UseCases.Implementation.Features.Commands.Persons;

internal sealed class CreatePersonCommandHandler(
    IPersonRepository personRepository,
    ICloudStorageService cloudStorageService)
    : IRequestHandler<CreatePersonCommand, Unit>
{
    public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();

        string? avatarUrl = null;

        if (request.PersonRequestDto.AvatarUrl != null)
            avatarUrl = request.PersonRequestDto.AvatarUrl;
        else if (request.PersonRequestDto.Avatar != null)
            avatarUrl = await cloudStorageService.UploadFileAsync(request.PersonRequestDto.Avatar, id);

        var person = new PersonEntity(id, request.PersonRequestDto.Name, request.PersonRequestDto.Description,
            avatarUrl, request.UserId, request.PersonRequestDto.Email, request.PersonRequestDto.PhoneNumber);

        await personRepository.AddAsync(person);

        await personRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}