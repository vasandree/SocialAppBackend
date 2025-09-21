using AuthModule.UseCases.Interfaces.Dtos.Responses;
using AutoMapper;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Dtos.Responses;

namespace UserModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramAccount.Id));
        CreateMap<ApplicationUser, ShortenUserDto>();
        CreateMap<UserSettings, UserSettingsDto>()
            .ForMember(settings => settings.LanguageCode, opt => opt.MapFrom(src => src.Language))
            .ForMember(settings => settings.Theme, opt => opt.MapFrom(src => src.Theme))
            .ForMember(settings => settings.EventReminders, opt => opt.MapFrom(src => src.EventNotifications))
            .ForMember(settings => settings.TaskReminders, opt => opt.MapFrom(src => src.TaskNotifications));
    }
}