using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Shared.Domain;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;

public interface ILoginCommandFactory
{
    IRequest<AuthResponse> Create(SocialNetwork type, InitDataDto initData);
}