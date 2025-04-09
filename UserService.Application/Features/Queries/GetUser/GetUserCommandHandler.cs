using AutoMapper;
using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Queries.GetUser;

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new NotFound("User not found");
        
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        return _mapper.Map<UserDto>(user);
    }
}