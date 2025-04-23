using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Dtos.Responses.Place;
using SocialNode.Contracts.Queries;
using SocialNode.Contracts.Repositories;

namespace SocialNode.Application.Features.Queries;

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