using Microsoft.EntityFrameworkCore;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure;

public class WorkspaceDbContext : DbContext
{
    private DbSet<WorkspaceEntity> Workspaces { get; set; }
    private DbSet<RelationEntity> WorkspaceRelations { get; set; }


    public WorkspaceDbContext(DbContextOptions<WorkspaceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}