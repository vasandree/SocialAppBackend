using AuthModule.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AuthModule.UseCases.Implementation.BackgroundTasks;

public class CleanupExpiredTokensJob(AuthDbContext dbContext, ILogger<CleanupExpiredTokensJob> logger)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var now = DateTime.UtcNow;
            var deletedCount = await dbContext.RefreshTokens
                .Where(token => token.Expires < now)
                .ExecuteDeleteAsync(); 

            if (deletedCount > 0)
            {
                logger.LogInformation("[Quartz] Удалено {Count} истекших refresh-токенов", deletedCount);
            }
            else
            {
                logger.LogInformation("[Quartz] Нет истекших refresh-токенов для удаления");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[Quartz] Ошибка при очистке refresh-токенов");
        }
    }
}