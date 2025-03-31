using AutoMapper;
using Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Entities;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Queries.GetUsers;

public class GetUsersCommandHAndler : IRequestHandler<GetUsersCommand, List<ShortenUserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersCommandHAndler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ShortenUserDto>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new NotFound("Provided user does not exist");

        var query = _userRepository.GetAllUsers();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(u =>
                EF.Functions.Like(u.UserName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(u.FirstName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(u.LastName, $"%{request.SearchTerm}%"));
        }

        var users = await query.ToListAsync(cancellationToken);

        return _mapper.Map(users, new List<ShortenUserDto>());
    }
}