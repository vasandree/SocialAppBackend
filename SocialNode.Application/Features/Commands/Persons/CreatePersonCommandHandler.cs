using MediatR;
using SocialNode.Contracts.Commands.Person;
using SocialNode.Contracts.Repositories;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;

namespace SocialNode.Application.Features.Commands.Persons;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Unit>
{
    private readonly IPersonRepository _personRepository;
    private readonly ICloudStorageService _cloudStorageService;

    public CreatePersonCommandHandler(IPersonRepository personRepository,
        ICloudStorageService cloudStorageService)
    {
        _personRepository = personRepository;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        var id = Guid.NewGuid();

        var person = new PersonEntity
        {
            Id = id,
            Name = request.PersonRequestDto.Name,
            Description = request.PersonRequestDto.Description,
            AvatarUrl = request.PersonRequestDto.Avatar != null
                ? await _cloudStorageService.UploadFileAsync(request.PersonRequestDto.Avatar, id)
                : null,
            CreatorId = request.UserId,
            Email = request.PersonRequestDto.Email,
            PhoneNumber = request.PersonRequestDto.PhoneNumber
        };

        await _personRepository.AddAsync(person);

        return Unit.Value;
    }
}