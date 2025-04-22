using System.ComponentModel.DataAnnotations;

namespace User.Contracts.Dtos.Requests;

public class InitDataDto
{
    [Required]
    public string InitData { get; set; }
}