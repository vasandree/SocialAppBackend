using AutoMapper;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.ClusterOfPeople;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonEntity, PersonDto>();
        CreateMap<Place, PlaceDto>();
        CreateMap<ClusterOfPeople, ClusterOfPeopleDto>();
        CreateMap<BaseSocialNode, ListedBaseSocialNodeDto>();
    }
}