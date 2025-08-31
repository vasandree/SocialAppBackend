using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;
using TaskModule.Domain.Entites;

namespace TaskModule.DataAccess.Implementation;

public sealed class TaskDbContext(DbContextOptions<TaskDbContext> options) : CommonDbContext(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }
}