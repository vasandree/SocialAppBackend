using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using AutoMapper;
using MediatR;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Commands;

namespace AuthModule.UseCases.Implementation.Features.Commands;

internal sealed class RegisterCommandHandler(
    IMapper mapper,
    ISender sender,
    IUserRepository userRepository,
    IUserSettingsRepository userSettingsRepository)
    : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await using var transaction =
            await userRepository.GetDbContext().Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var user = await sender.Send(new AddUserCommand(null, new InitDataDto(request.RegisterDto.ToString())),
                cancellationToken);

            return new AuthResponse(await sender.Send(new CreateTokensCommand(user), cancellationToken),
                mapper.Map<UserSettingsDto>(await userSettingsRepository.GetByUserIdAsync(user.Id)));
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            userRepository.ClearChanges();
        }
    }
}