using AutoMapper;
using SocialNode.Contracts.Dtos.Responses;
using SocialNode.Contracts.Dtos.Responses.ClusterOfPeople;
using SocialNode.Contracts.Dtos.Responses.Person;
using SocialNode.Contracts.Dtos.Responses.Place;
using SocialNode.Domain.Entities;

namespace SocialNode.Application;

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