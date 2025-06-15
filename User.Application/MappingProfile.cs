using AutoMapper;
using User.Contracts.Dtos;
using User.Contracts.Dtos.Responses;
using User.Domain.Entities;

namespace User.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramAccount.Id));
        CreateMap<ApplicationUser, ShortenUserDto>();
        CreateMap<UserSettings, UserSettingsDto>();
    }
}