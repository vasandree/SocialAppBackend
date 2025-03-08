using Common.Exceptions;
using MediatR;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfUserExistsByTelegramIdAsync(request.User.Id))
            throw new BadRequest("User does not exist");
        
        var user = await _userRepository.GetByTelegramIdAsync(request.User.Id);

        user.UserName = request.User.Username;
        user.FirstName = request.User.First_Name;
        user.LastName = request.User.Last_Name;
        user.PhotoUrl = request.User.Photo_Url;
        user.LanguageCode = request.User.Language_Code;
        
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}