using AutoMapper;
using PersonService.Application.Dtos.Responses;
using PersonService.Domain;
using ClusterOfPeople = PersonService.Domain.ClusterOfPeople;

namespace PersonService.Application;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Place, PlaceDto>();
        CreateMap<ClusterOfPeople, ClusterOfPeopleDto>();
    }
}