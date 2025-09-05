using System.Text.Json;
using AuthModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.UseCases.Interfaces.Commands.Person;
using SocialNodeModule.UseCases.Interfaces.Dtos.Requests;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser;

public class AddCommonUserHandler(ISender sender, IUserRepository userRepository)
    : IRequestHandler<AddCommonUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(AddCommonUserCommand request, CancellationToken cancellationToken)
    {
        await using var transaction =
            await userRepository.GetDbContext().Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var dto = JsonSerializer.Deserialize<RegisterDto>(request.InitDataDto.InitData)
                      ?? throw new Exception("Failed to deserialize RegisterDto");

            if (await userRepository.CheckIfUserExistsByEmailAsync(dto.Email))
                throw new Conflict("User with this email already exists");
            
            if (await userRepository.CheckIfUserExistsByUsernameAsync(dto.Username))
                throw new Conflict("User with this username already exists");

            var user = new ApplicationUser(dto.Email, dto.FirstName, dto.LastName,
                dto.Username, BCrypt.Net.BCrypt.HashPassword(dto.Password));

            var userSettings = new UserSettings(user);

            user.AddSettings(userSettings);

            await userRepository.AddAsync(user);

            await sender.Send(
                new CreatePersonCommand(user.Id,
                    new PersonRequestDto($"{user.FirstName} {user.LastName} (вы)", null, null, null, null,
                        user.PhotoUrl)),
                cancellationToken);

            await userRepository.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return user;
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