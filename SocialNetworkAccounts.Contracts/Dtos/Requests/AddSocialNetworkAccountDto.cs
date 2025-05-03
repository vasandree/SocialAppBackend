using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNetworkAccounts.Contracts.Dtos.Requests;

public class AddSocialNetworkAccountDto
{
    [Required] public SocialNetwork Type { get; init; }


    [Required] public required string Username { get; init; }
}