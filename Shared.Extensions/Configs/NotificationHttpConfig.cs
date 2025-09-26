namespace Shared.Extensions.Configs;

public class NotificationHttpConfig
{
    public string BaseUrl { get; set; } = string.Empty; 
    public string? EndpointPath { get; set; } 
    public int TimeoutSeconds { get; set; } = 10;
    public bool UseFormUrlEncoded { get; set; } = true; 
    public string? ParseMode { get; set; } = "Markdown";

    // --- Resilience ---
    public int MaxRetries { get; set; } = 3; // включая первую попытку
    public int RetryBaseDelayMilliseconds { get; set; } = 500; // базовая задержка перед повтором
}
