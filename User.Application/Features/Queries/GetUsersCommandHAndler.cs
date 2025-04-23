using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using User.Contracts.Dtos.Responses;
using User.Contracts.Queries;
using User.Contracts.Repositories;

namespace User.Application.Features.Queries;

public class GetUsersCommandHAndler : IRequestHandler<GetUsersQuery, List<ShortenUserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersCommandHAndler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<ShortenUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
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