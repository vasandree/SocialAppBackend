using System.ComponentModel.DataAnnotations;

namespace TaskModule.Contracts.Dtos.Responses;

public class ListedTaskDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public Guid SocialNodeId { get; set; }
}