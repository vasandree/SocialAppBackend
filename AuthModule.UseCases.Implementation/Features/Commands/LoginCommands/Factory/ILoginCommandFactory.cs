using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using Shared.Domain;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;

public interface ILoginCommandFactory
{
    IRequest<AuthResponse> Create(SocialNetwork type, InitDataDto initData);
}