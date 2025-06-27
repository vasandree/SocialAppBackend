using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Extensions.Configs;
using SocialNode.Contracts.Dtos.Responses;
using SocialNode.Contracts.Dtos.Responses.Person;
using SocialNode.Contracts.Queries;
using SocialNode.Contracts.Repositories;

namespace SocialNode.Application.Features.Queries;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PaginatedPersonsDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetPersonsQueryHandler(IPersonRepository personRepository, IMapper mapper, IOptions<PaginationConfig> config)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _pageSize = config.Value.PageSize;
    }

    public async Task<PaginatedPersonsDto> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAllByUserId(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            persons = persons.Where(person => person.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }


        var totalCount = await persons.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _pageSize));

        var result = persons
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize)
            .ToList();

        return new PaginatedPersonsDto()
        {
            Person = _mapper.Map<List<ListedBaseSocialNodeDto>>(result),
            Pagination = new Pagination(_pageSize, request.Page, totalPages)
        };
    }
}