using AutoMapper;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.UseCases.Implementation;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UsersAccount, SocialNetworkAccountDto>();
        CreateMap<PersonsAccount, SocialNetworkAccountDto>();
    }
}