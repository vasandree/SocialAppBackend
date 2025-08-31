using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Dtos;
using Shared.Extensions.Configs;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetPlacesQueryHandler(
    IPlaceRepository placeRepository,
    IMapper mapper,
    IOptions<PaginationConfig> configuration)
    : IRequestHandler<GetPlacesQuery, PaginatedPlacesDto>
{
    private readonly int _pageSize = configuration.Value.PageSize;

    public async Task<PaginatedPlacesDto> Handle(GetPlacesQuery request, CancellationToken cancellationToken)
    {
        var places = await placeRepository.GetAllByUSerId(request.UserId);

        if (!string.IsNullOrEmpty(request.Name))
        {
            places = places.Where(person => EF.Functions.Like(person.Name, $"%{request.Name}%"));
        }


        var totalCount = await places.CountAsync(cancellationToken);
        var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / _pageSize));

        places = places
            .Skip((request.Page - 1) * _pageSize)
            .Take(_pageSize);

        return new PaginatedPlacesDto(mapper.Map<List<ListedBaseSocialNodeDto>>(places),
            new Pagination(_pageSize, request.Page, totalPages));
    }
}