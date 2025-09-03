using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using AutoMapper;
using MediatR;
using Shared.Domain;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Helpers;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands;

internal sealed class LoginWithTelegramCommandHandler(
    ITelegramHelper telegramHelper,
    ISender mediator,
    ITelegramAccountRepository telegramAccountRepository,
    IUsersAccountRepository usersAccountRepository,
    IUserSettingsRepository userSettingsRepository,
    IMapper mapper)
    : IRequestHandler<LoginWithTelegramCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginWithTelegramCommand request, CancellationToken cancellationToken)
    {
        if (request.InitData == null)
            throw new BadRequest("No InitData provided");

        if (!telegramHelper.ValidateInitData(request.InitData.InitData))
            throw new Unauthorized("Failed to authenticate");

        var parsedInitData = telegramHelper.ParseInitData(request.InitData.InitData);

        if (parsedInitData.User == null) throw new BadRequest("Invalid InitData");

        await using var transaction =
            await usersAccountRepository.GetDbContext().Database.BeginTransactionAsync(cancellationToken);

        try
        {
            ApplicationUser? user;
            if (await telegramAccountRepository.CheckIfUserExistsByTelegramIdAsync(parsedInitData.User.Id))
            {
                var userByTelegramIdAsync =
                    await telegramAccountRepository.GetUserByTelegramIdAsync(parsedInitData.User.Id);

                user = await mediator.Send(
                    new UpdateUserCommand(userByTelegramIdAsync.Id, SocialNetwork.Telegram, request.InitData),
                    cancellationToken);

                var socialNetworkAccount =
                    await usersAccountRepository.GetByUserIdAndTypeAsync(user.Id, SocialNetwork.Telegram);

                if (socialNetworkAccount != null)
                    await mediator.Send(new EditSocialNetworkAccountCommand(user.Id, socialNetworkAccount.Id,
                        new EditSocialNetworkAccountDto(user.UserName)), cancellationToken);
            }
            else
            {
                user = await mediator.Send(new AddUserCommand(SocialNetwork.Telegram, request.InitData),
                    cancellationToken);

                await mediator.Send(
                    new AddSocialNetworkAccountCommand(user.Id,
                        new AddSocialNetworkAccountDto(SocialNetwork.Telegram, user.UserName)), cancellationToken);
            }

            var tokens = await mediator.Send(new CreateTokensCommand(user), cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new AuthResponse(tokens,
                mapper.Map<UserSettingsDto>(await userSettingsRepository.GetByUserIdAsync(user.Id)));
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            usersAccountRepository.ClearChanges();
        }
    }
}