using Shared.Domain;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Requests;

public record AddSocialNetworkAccountDto(SocialNetwork Type, string Username);
