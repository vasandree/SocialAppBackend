using MediatR;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Helpers;

namespace UserModule.UseCases.Implementation.Features.Commands.UpdateUser;

internal sealed class UpdateTelegramUserCommandHandler(
    ITelegramHelper telegramHelper,
    ITelegramAccountRepository telegramAccountRepository)
    : IRequestHandler<UpdateTelegramUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(UpdateTelegramUserCommand request, CancellationToken cancellationToken)
    {
        var parsedInitData = telegramHelper.ParseInitData(request.InitData.InitData);

        var user = await telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);

        user.UpdateInfo(parsedInitData.User.Username, parsedInitData.User.First_Name, parsedInitData.User.Last_Name,
            parsedInitData.User.Photo_Url);

        var telegramAccount = await telegramAccountRepository.GetByTelegramIdAsync(parsedInitData.User.Id);

        telegramAccount.UpdateInfo(parsedInitData.User.Username, parsedInitData.User.First_Name,
            parsedInitData.User.Last_Name, parsedInitData.User.Photo_Url, parsedInitData.User.Language_Code,
            parsedInitData.User.Allows_Write_To_Pm);

        await telegramAccountRepository.SaveChangesAsync(cancellationToken);

        return user;
    }
}