using AutoMapper;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Dtos;
using UserModule.UseCases.Interfaces.Dtos.Responses;

namespace UserModule.UseCases.Implementation;

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