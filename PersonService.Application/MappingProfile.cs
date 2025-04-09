using AutoMapper;
using PersonService.Application.Dtos.Responses;
using PersonService.Application.Dtos.Responses.ClusterOfPeople;
using PersonService.Application.Dtos.Responses.Person;
using PersonService.Application.Dtos.Responses.Place;
using PersonService.Domain;
using PersonService.Domain.Entities;
using ClusterOfPeople = PersonService.Domain.Entities.ClusterOfPeople;

namespace PersonService.Application;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Place, PlaceDto>();
        CreateMap<ClusterOfPeople, ClusterOfPeopleDto>();
        CreateMap<BaseSocialNode, ListedBaseSocialNodeDto>();
    }
}