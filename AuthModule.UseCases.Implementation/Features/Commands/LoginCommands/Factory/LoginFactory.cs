using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using Shared.Domain;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;

public class LoginFactory : ILoginCommandFactory
{
    public IRequest<AuthResponse> Create(SocialNetwork type, InitDataDto initData) => type switch
    {
        SocialNetwork.Telegram => new LoginWithTelegramCommand(initData),
        _ => throw new NotSupportedException($"Login for {type} is not supported")
    };
}