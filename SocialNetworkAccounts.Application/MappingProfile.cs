using AutoMapper;
using SocialNetworkAccounts.Application.Dtos.Responses;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Application;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UsersAccount, SocialNetworkAccountDto>();
        CreateMap<PersonsAccount, SocialNetworkAccountDto>();
    }
}