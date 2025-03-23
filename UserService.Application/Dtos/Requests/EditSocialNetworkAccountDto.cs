using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Requests;

public class EditSocialNetworkAccountDto
{
    [Required] public string Url { get; set; }

    [Required] public string Username { get; set; }
}