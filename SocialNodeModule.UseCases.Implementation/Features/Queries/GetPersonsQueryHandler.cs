using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Extensions.Configs;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetPersonsQueryHandler(
    IPersonRepository personRepository,
    IMapper mapper,
    IOptions<PaginationConfig> config)
    : IRequestHandler<GetPersonsQuery, PaginatedPersonsDto>
{
    private readonly int _pageSize = config.Value.PageSize;

    public async Task<PaginatedPersonsDto> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await personRepository.GetAllByUserId(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            persons = persons.Where(person => EF.Functions.Like(person.Name, $"%{request.Name}%"));
        }


        var totalCount = await persons.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _pageSize));

        var result = persons
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginatedPersonsDto(mapper.Map<List<ListedBaseSocialNodeDto>>(result),
            new Pagination(_pageSize, request.Page, totalPages));
    }
}