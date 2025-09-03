using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Shared.Domain;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;

public class LoginFactory : ILoginCommandFactory
{
    public IRequest<AuthResponse> Create(SocialNetwork type, InitDataDto initData) => type switch
    {
        SocialNetwork.Telegram => new LoginWithTelegramCommand(initData),
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
        _ => new CommonLoginCommand(initData)
    };
}