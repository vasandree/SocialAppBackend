using AutoMapper;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Entities;

namespace UserService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.TelegramId, opt => opt.MapFrom(src => src.TelegramAccount.Id));
        CreateMap<User, ShortenUserDto>();
    }
}