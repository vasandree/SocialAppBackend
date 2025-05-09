using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using User.Contracts.Dtos.Responses;
using User.Contracts.Queries;
using User.Contracts.Repositories;
using User.Domain.Entities;

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