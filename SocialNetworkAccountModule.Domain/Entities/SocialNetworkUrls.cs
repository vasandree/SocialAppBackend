using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccountModule.Domain.Entities;

public class SocialNetworkUrls
{
    private SocialNetworkUrls() { }

    public SocialNetworkUrls(SocialNetwork socialNetwork, string baseUrl)
    {
        Type = socialNetwork;
        Url = baseUrl;
    }
    
    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; init; }

    [Required]
    public string Url { get; set; }
    
    public void ChangeUrl(string url) => Url = url;
}