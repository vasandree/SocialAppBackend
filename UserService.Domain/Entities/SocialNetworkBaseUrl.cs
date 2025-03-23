using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class SocialNetworkBaseUrl
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [EnumDataType(typeof(SocialNetwork))]
    public SocialNetwork Type { get; set; }
    
    [Required]
    public string Url { get; set; }
}