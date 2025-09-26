using MediatR;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Helpers;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser;

internal sealed class AddTelegramUserCommandHandler(
    IUserRepository userRepository,
    ITelegramHelper telegramHelper,
    ITelegramAccountRepository telegramAccountRepository,
    ISender mediator)
    : IRequestHandler<AddTelegramUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(AddTelegramUserCommand request,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await userRepository.GetDbContext().Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var parsedTelegramData = telegramHelper.ParseInitData(request.InitData.InitData);

            if (parsedTelegramData.User == null) throw new NullReferenceException(nameof(parsedTelegramData));

            var user = new ApplicationUser(parsedTelegramData.User.First_Name, parsedTelegramData.User.Last_Name,
                parsedTelegramData.User.Username, parsedTelegramData.User.Photo_Url);

            var telegramAccount = new TelegramAccount(parsedTelegramData.User.Id, parsedTelegramData.User.Username,
                parsedTelegramData.User.First_Name, parsedTelegramData.User.Last_Name,
                parsedTelegramData.User.Language_Code, parsedTelegramData.User.Allows_Write_To_Pm,
                parsedTelegramData.User.Photo_Url, user);

            var userSettings = new UserSettings(user, telegramHelper.GetLanguage(parsedTelegramData.User.Language_Code),
                user.TelegramAccount!.Id);

            user.AddTelegramAccount(telegramAccount);
            user.AddSettings(userSettings);

            await userRepository.AddAsync(user);
            await telegramAccountRepository.AddAsync(telegramAccount);

            await mediator.Send(
                new CreatePersonCommand(user.Id,
                    new PersonRequestDto($"{user.FirstName} {user.LastName} (вы)", null, null, null, null,
                        user.PhotoUrl)),
                cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            
            return user;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}