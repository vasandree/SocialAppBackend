using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.DataAccess.Implementation;

public sealed class WorkspaceDbContext(DbContextOptions<WorkspaceDbContext> options) : CommonDbContext(options)
{
    public DbSet<WorkspaceEntity> Workspaces { get; set; }
    public DbSet<RelationEntity> WorkspaceRelations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}