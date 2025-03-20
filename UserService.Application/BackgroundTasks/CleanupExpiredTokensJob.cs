using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using UserService.Persistence;

namespace UserService.Application.BackgroundTasks;

public class CleanupExpiredTokensJob : IJob
{
    private readonly UserServiceDbContext _dbContext;
    private readonly ILogger<CleanupExpiredTokensJob> _logger;

    public CleanupExpiredTokensJob(UserServiceDbContext dbContext, ILogger<CleanupExpiredTokensJob> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var now = DateTime.UtcNow;
            int deletedCount = await _dbContext.RefreshTokens
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