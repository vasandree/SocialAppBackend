using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.Tests.Entities;

public class SocialNetworkUrlsTests
{
    [Fact]
    public void Constructor_SetsTypeAndUrl()
    {
        // Arrange
        var type = SocialNetwork.Telegram;
        var url = "https://t.me/example";

        // Act
        var entity = new SocialNetworkUrls(type, url);

        // Assert
        Assert.Equal(type, entity.Type);
        Assert.Equal(url, entity.Url);
    }

    [Fact]
    public void ChangeUrl_ShouldUpdateUrl()
    {
        // Arrange
        var entity = new SocialNetworkUrls(
            SocialNetwork.Telegram,
            "https://old.link");

        var newUrl = "https://new.link";

        // Act
        entity.ChangeUrl(newUrl);

        // Assert
        Assert.Equal(newUrl, entity.Url);
    }
}