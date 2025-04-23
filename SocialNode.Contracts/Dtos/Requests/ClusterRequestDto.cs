using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNode.Contracts.Dtos.Requests;

public class ClusterRequestDto
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public IFormFile? Avatar { get; set; }

    public List<Guid>? Users { get; set; } = [];
}