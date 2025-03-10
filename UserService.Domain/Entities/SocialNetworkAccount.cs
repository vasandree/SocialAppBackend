using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class SocialNetworkAccount
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string Username {get; set;}

    [Required] 
    public string Url {get; set;}
    
    [Required]
    public SocialNetwork Type {get; set;}
    
    [Required]
    public User User {get; set;}
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId {get; set;}
}