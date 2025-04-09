using AutoMapper;
using Common.Models.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using PersonService.Application.Dtos.Responses;
using PersonService.Application.Dtos.Responses.Person;
using PersonService.Persistence.Repositories.PersonRepository;

namespace PersonService.Application.Features.Queries.GetPersonsQuery;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PaginatedPersonsDto>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetPersonsQueryHandler(IPersonRepository personRepository, IMapper mapper,  IConfiguration configuration)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _pageSize = configuration.GetValue<int>("PageSize");
    }

    public async Task<PaginatedPersonsDto> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var persosns = await _personRepository.GetAllAsQueryable(request.UserId);
        
        if (!string.IsNullOrEmpty(request.Name))
        {
            persosns = persosns.Where(cluster => cluster.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalPages = (int)Math.Ceiling((double)persosns.Count() / _pageSize);
        
        persosns = persosns
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize);
        
        return new PaginatedPersonsDto()
        {
            Person = _mapper.Map<List<ListedBaseSocialNodeDto>>(persosns),
            Pagination = new Pagination(_pageSize, request.Page, totalPages)
        };
    }
}