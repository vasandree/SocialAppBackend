namespace Shared.Extensions.Configs;

public class NotificationResilienceConfig
{
    public int MaxRetryAttempts { get; set; } = 5;
    public int RetryDelayMilliseconds { get; set; } = 200;
    public string RetryBackoffType { get; set; } = "Exponential"; 
    public bool RetryUseJitter { get; set; } = true;

    public double CircuitBreakerFailureRatio { get; set; } = 0.5;
    public int CircuitBreakerMinimumThroughput { get; set; } = 10;
    public int CircuitBreakerBreakDurationSeconds { get; set; } = 30;

    public int PollyTimeoutSeconds { get; set; } = 10;
    public int ConcurrencyPermitLimit { get; set; } = 5;
}

