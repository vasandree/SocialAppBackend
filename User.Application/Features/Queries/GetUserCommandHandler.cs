using AutoMapper;
using Common.Exceptions;
using MediatR;
using User.Contracts.Dtos.Responses;
using User.Contracts.Queries;
using User.Contracts.Repositories;

namespace User.Application.Features.Queries;

public class GetUserCommandHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new NotFound("User not found");
        
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        return _mapper.Map<UserDto>(user);
    }
}