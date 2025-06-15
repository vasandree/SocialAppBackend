using System.ComponentModel.DataAnnotations;
using User.Domain.Enums;

namespace User.Contracts.Dtos;

public record UserSettingsDto
{
    [Required]
    public Theme Theme { get; set; }
    
    [Required] 
    public Language LanguageCode { get; set; }
}