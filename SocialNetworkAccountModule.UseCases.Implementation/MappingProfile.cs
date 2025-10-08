using AutoMapper;
using Shared.Domain;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.UseCases.Implementation;

public class MappingProfile : Profile
{
    private static readonly Dictionary<SocialNetwork, string> SocialBaseUrls = new()
    {
        { SocialNetwork.Facebook, "https://www.facebook.com/" },
        { SocialNetwork.Twitter, "https://x.com/" },
        { SocialNetwork.Instagram, "https://www.instagram.com/" },
        { SocialNetwork.LinkedIn, "https://www.linkedin.com/in/" },
        { SocialNetwork.YouTube, "https://www.youtube.com/" },
        { SocialNetwork.Pinterest, "https://www.pinterest.com/" },
        { SocialNetwork.Snapchat, "https://www.snapchat.com/add/" },
        { SocialNetwork.TikTok, "https://www.tiktok.com/@" },
        { SocialNetwork.Reddit, "https://www.reddit.com/user/" },
        { SocialNetwork.WhatsApp, "https://wa.me/" },
        { SocialNetwork.Telegram, "https://t.me/" },
        { SocialNetwork.Twitch, "https://www.twitch.tv/" }
    };

    private static string BuildUrl(SocialNetwork type, string username)
    {
        return SocialBaseUrls.TryGetValue(type, out var baseUrl)
            ? $"{baseUrl}{username}"
            : username;
    }

    public MappingProfile()
    {
        CreateMap<UsersAccount, SocialNetworkAccountDto>()
            .ForMember(dest => dest.Url,
                opt => opt.MapFrom(src => BuildUrl(src.Type, src.Username)));

        CreateMap<PersonsAccount, SocialNetworkAccountDto>()
            .ForMember(dest => dest.Url,
                opt => opt.MapFrom(src => BuildUrl(src.Type, src.Username)));
    }
}