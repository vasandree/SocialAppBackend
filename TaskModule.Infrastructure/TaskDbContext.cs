using Microsoft.EntityFrameworkCore;
using TaskModule.Domain.Entites;

namespace TaskModule.Infrastructure;

public class TaskDbContext : DbContext
{
    private DbSet<TaskEntity> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }
}