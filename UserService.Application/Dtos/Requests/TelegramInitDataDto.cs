using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Requests;

public class TelegramInitDataDto
{
    [Required]
    public string InitData { get; set; }
}