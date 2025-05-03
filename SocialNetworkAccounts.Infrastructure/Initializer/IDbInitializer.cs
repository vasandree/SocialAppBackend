namespace SocialNetworkAccounts.Infrastructure.Initializer;

public interface IDbInitializer
{
    Task InitializeAsync();
}