using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Extensions.Configs;
using SocialNode.Contracts.Dtos.Responses;
using SocialNode.Contracts.Dtos.Responses.Place;
using SocialNode.Contracts.Queries;
using SocialNode.Contracts.Repositories;

namespace SocialNode.Application.Features.Queries;

public class GetPlacesQueryHandler : IRequestHandler<GetPlacesQuery, PaginatedPlacesDto>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly IMapper _mapper;
    private readonly int _pageSize;

    public GetPlacesQueryHandler(IPlaceRepository placeRepository, IMapper mapper, IOptions<PaginationConfig> configuration)
    {
        _placeRepository = placeRepository;
        _mapper = mapper;
        _pageSize = configuration.Value.PageSize;
    }

    public async Task<PaginatedPlacesDto> Handle(GetPlacesQuery request, CancellationToken cancellationToken)
    {
        var places = await _placeRepository.GetAllByUSerId(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            places = places.Where(person => EF.Functions.Like(person.Name, $"%{request.Name}%"));
        }


        var totalCount = await places.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _pageSize));

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