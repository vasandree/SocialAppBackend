using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;
using SocialNodeModule.UseCases.Interfaces.Queries;

namespace SocialNodeModule.UseCases.Implementation.Features.Queries;

internal sealed class GetPlaceQueryHandler(IPlaceRepository placeRepository, IMapper mapper)
    : IRequestHandler<GetPlaceQuery, PlaceDto>
{
    public async Task<PlaceDto> Handle(GetPlaceQuery request, CancellationToken cancellationToken)
    {
       if(!await placeRepository.CheckIfExists(request.PlaceId))
           throw new NotFound("Provided place not found");
       
       if(!await placeRepository.CheckIfUserIsCreator(request.UserId, request.PlaceId))
           throw new Forbidden("You are not allowed to see this place");
       
       var place = await placeRepository.GetByIdAsync(request.PlaceId);
       return mapper.Map<PlaceDto>(place); 
    }
}