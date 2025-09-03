using System.Text.Json;
using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.DataAccess.Interfaces.Repositories;

namespace AuthModule.UseCases.Implementation.Features.Commands.LoginCommands;

internal sealed class CommonLoginCommandHandler(
    ISender sender,
    IMapper mapper,
    IUserRepository userRepository,
    IUserSettingsRepository userSettingsRepository
)
    : IRequestHandler<CommonLoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(CommonLoginCommand request, CancellationToken cancellationToken)
    {
        var dto = JsonSerializer.Deserialize<CommonLoginDto>(request.InitDataDto.InitData)
                  ?? throw new Exception("Unable to deserialize CommonLoginDto");

        var user = await userRepository.GetByUsernameAsync(dto.Login) ??
                   await userRepository.GetByEmailAsync(dto.Login);

        if (user == null) throw new BadRequest("User with provided login doesn't exist");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new BadRequest("Password is incorrect");

        return new AuthResponse(await sender.Send(new CreateTokensCommand(user), cancellationToken),
            mapper.Map<UserSettingsDto>(await userSettingsRepository.GetByUserIdAsync(user.Id)));
    }
}