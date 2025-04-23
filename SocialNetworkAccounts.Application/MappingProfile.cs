using AutoMapper;
using SocialNetworkAccounts.Contracts.Dtos.Responses;
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