using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Dtos.Responses;
using UserModule.UseCases.Interfaces.Queries;

namespace UserModule.UseCases.Implementation.Features.Queries;

internal sealed class GetUsersCommandHAndler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetUsersQuery, List<ShortenUserDto>>
{
    public async Task<List<ShortenUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        if (!await userRepository.CheckIfExists(request.UserId))
            throw new NotFound("Provided user does not exist");

        var query = userRepository.GetAllUsers();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Where(u =>
                EF.Functions.Like(u.UserName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(u.FirstName, $"%{request.SearchTerm}%") ||
                EF.Functions.Like(u.LastName, $"%{request.SearchTerm}%"));
        }

        var users = await query.ToListAsync(cancellationToken);

        return mapper.Map(users, new List<ShortenUserDto>());
    }
}