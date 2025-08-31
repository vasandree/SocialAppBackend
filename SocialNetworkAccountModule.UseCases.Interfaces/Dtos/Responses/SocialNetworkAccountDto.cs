using Shared.Domain;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;

public record SocialNetworkAccountDto(Guid Id, SocialNetwork Type, string Url, string Username);