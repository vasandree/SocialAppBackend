using System.ComponentModel.DataAnnotations;

namespace PersonService.Application.Dtos.Requests;

public class ClusterRequestDto
{
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? avatarUrl { get; set; }

    public List<Guid>? Users { get; set; } = [];
}