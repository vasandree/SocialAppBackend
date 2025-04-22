using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Auth.Application.BackgroundTasks;

public class CleanupExpiredTokensJob : IJob
{
    private readonly AuthDbContext _dbContext;
    private readonly ILogger<CleanupExpiredTokensJob> _logger;

    public CleanupExpiredTokensJob(AuthDbContext dbContext, ILogger<CleanupExpiredTokensJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var now = DateTime.UtcNow;
            var deletedCount = await _dbContext.RefreshTokens
                .Where(token => token.Expires < now)
                .ExecuteDeleteAsync(); 

            if (deletedCount > 0)
            {
                _logger.LogInformation("[Quartz] Удалено {Count} истекших refresh-токенов", deletedCount);
            }
            else
            {
                _logger.LogInformation("[Quartz] Нет истекших refresh-токенов для удаления");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[Quartz] Ошибка при очистке refresh-токенов");
        }
    }
}