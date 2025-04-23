using System.ComponentModel.DataAnnotations;

namespace SocialNode.Contracts.Dtos.Responses;

public class ListedBaseSocialNodeDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set;}
    
    
    public string? avatarUrl { get; set; }
}