using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Dtos.Responses;
using UserModule.UseCases.Interfaces.Queries;

namespace UserModule.UseCases.Implementation.Features.Queries;

internal sealed class GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (!await userRepository.CheckIfExists(request.UserId))
            throw new NotFound("User not found");
        
        var user = await userRepository.GetByIdAsync(request.UserId);
        
        return mapper.Map<UserDto>(user);
    }
}