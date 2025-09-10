using Shared.Domain;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;

public record SocialNetworkAccountDto
{
    public Guid Id { get; init; }
    public SocialNetwork Type { get; init; }
    public string Url { get; init; }
    public string Username { get; init; }
};