using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccounts.Domain.Entities;

public class SocialNetworkUrls
{

    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; set; }
    
    [Required]
    public string Url { get; set; }
}