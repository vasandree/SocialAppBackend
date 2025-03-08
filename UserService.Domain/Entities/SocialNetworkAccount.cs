using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class SocialNetworkAccount
{
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