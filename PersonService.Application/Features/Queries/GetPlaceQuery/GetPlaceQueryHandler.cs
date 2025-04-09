using AutoMapper;
using Common.Exceptions;
using MediatR;
using PersonService.Application.Dtos.Responses.Place;
using PersonService.Persistence.Repositories.PlaceRepository;

namespace PersonService.Application.Features.Queries.GetPlaceQuery;

public class GetPlaceQueryHandler : IRequestHandler<GetPlaceQuery, PlaceDto>
{
    private readonly IPlaceRepository _placeRepository;
    private IMapper _mapper;

    public GetPlaceQueryHandler(IPlaceRepository placeRepository, IMapper mapper)
    {
        _placeRepository = placeRepository;
        _mapper = mapper;
    }

    public async Task<PlaceDto> Handle(GetPlaceQuery request, CancellationToken cancellationToken)
    {
       if(!await _placeRepository.CheckIfExists(request.PlaceId))
           throw new NotFound("Provided place not found");
       
       if(!await _placeRepository.CheckIfUserIsCreator(request.UserId, request.PlaceId))
           throw new Forbidden("You are not allowed to see this place");
       
       var place = await _placeRepository.GetByIdAsync(request.PlaceId);
       return _mapper.Map<PlaceDto>(place); 
    }
}