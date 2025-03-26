using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Requests;

public class InitDataDto
{
    [Required]
    public string InitData { get; set; }
}