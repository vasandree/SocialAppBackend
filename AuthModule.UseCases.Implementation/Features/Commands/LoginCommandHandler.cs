using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Implementation.Features.Commands.LoginCommands.Factory;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace AuthModule.UseCases.Implementation.Features.Commands;

internal sealed class LoginCommandHandler(ISender mediator, ILoginCommandFactory factory)
    : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var command = factory.Create(request.Type, request.InitData);
        return await mediator.Send(command, cancellationToken);
    }
}