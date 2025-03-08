using Common.Exceptions;
using MediatR;
using UserService.Domain;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.CheckIfUserExistsByTelegramIdAsync(request.InitData.Id))
            throw new BadRequest("Such user already exists");

        var user = new User()
        {
            TelegramId = request.InitData.Id,
            FirstName = request.InitData.First_Name,
            LastName = request.InitData.Last_Name,
            UserName = request.InitData.Username,
            PhotoUrl = request.InitData.Photo_Url,
            LanguageCode = request.InitData.Language_Code
        };
        
        await _userRepository.AddAsync(user);
        
        return user;
    }
}