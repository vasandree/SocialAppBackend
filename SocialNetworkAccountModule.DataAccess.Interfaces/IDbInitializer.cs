namespace SocialNetworkAccountModule.DataAccess.Interfaces;

public interface IDbInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken);
}