using Microsoft.EntityFrameworkCore;

namespace Shared.DataAccess.Implementation;

public abstract class CommonDbContext(DbContextOptions options) : DbContext(options)
{
    public void ClearChanges() => ChangeTracker.Clear();
}