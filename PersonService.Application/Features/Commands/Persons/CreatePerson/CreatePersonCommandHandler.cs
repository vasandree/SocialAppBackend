using MediatR;
using PersonService.Domain.Entities;
using PersonService.Infrastructure.CloudStorage;
using PersonService.Persistence.Repositories.PersonRepository;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Application.Features.Commands.Persons.CreatePerson;

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

        var person = new Person
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