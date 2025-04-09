using AutoMapper;
using Common.Models.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using PersonService.Application.Dtos.Responses;
using PersonService.Application.Dtos.Responses.Place;
using PersonService.Persistence.Repositories.PlaceRepository;

namespace PersonService.Application.Features.Queries.GetPlacesQuery;

public class GetPlacesQueryHandler : IRequestHandler<GetPlacesQuery, PaginatedPlacesDto>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetPlacesQueryHandler(IPlaceRepository placeRepository, IMapper mapper,  IConfiguration configuration)
    {
        _placeRepository = placeRepository;
        _mapper = mapper;
        _pageSize = configuration.GetValue<int>("PageSize");
    }

    public async Task<PaginatedPlacesDto> Handle(GetPlacesQuery request, CancellationToken cancellationToken)
    {
        var places = await _placeRepository.GetAllAsQueryable(request.UserId);
        
        if (!string.IsNullOrEmpty(request.Name))
        {
            places = places.Where(place => place.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }
        
        var totalPages = (int)Math.Ceiling((double)places.Count() / _pageSize);
        
        places = places
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize);
        
        return new PaginatedPlacesDto()
        {
            Place = _mapper.Map<List<ListedBaseSocialNodeDto>>(places),
            Pagination = new Pagination(_pageSize, request.Page, totalPages)
        };
    }
}