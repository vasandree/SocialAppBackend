using AuthModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser.Factory;

internal sealed class AddUserCommandFactory : IAddUserCommandFactory
{
    public IRequest<ApplicationUser> Create(SocialNetwork? type, InitDataDto initData)
        => type switch
        {
            SocialNetwork.Telegram => new AddTelegramUserCommand(initData),
            SocialNetwork.Facebook => throw new NotImplementedException(),
            SocialNetwork.Twitter => throw new NotImplementedException(),
            SocialNetwork.LinkedIn => throw new NotImplementedException(),
            SocialNetwork.Instagram => throw new NotImplementedException(),
            SocialNetwork.YouTube => throw new NotImplementedException(),
            SocialNetwork.Pinterest => throw new NotImplementedException(),
            SocialNetwork.Snapchat => throw new NotImplementedException(),
            SocialNetwork.TikTok => throw new NotImplementedException(),
            SocialNetwork.Reddit => throw new NotImplementedException(),
            SocialNetwork.WhatsApp => throw new NotImplementedException(),
            SocialNetwork.GitHub => throw new NotImplementedException(),
            SocialNetwork.Twitch => throw new NotImplementedException(),
            SocialNetwork.Vk => throw new NotImplementedException(),
            _ => new AddCommonUserCommand(initData)
        };
}