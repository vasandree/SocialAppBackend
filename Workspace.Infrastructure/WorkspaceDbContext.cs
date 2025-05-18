using Microsoft.EntityFrameworkCore;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure;

public class WorkspaceDbContext : DbContext
{
    private DbSet<WorkspaceEntity> Workspaces { get; set; }
    private DbSet<Relation> WorkspaceRelations { get; set; }
    private DbSet<WorkspaceUnit> WorkspaceUnits { get; set; }
    private DbSet<WorkspacePerson> WorkspacePersons { get; set; }

    public WorkspaceDbContext(DbContextOptions<WorkspaceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkspaceUnit>()
            .HasOne(x => x.WorkspaceEntity)
            .WithMany(x => x.Units)
            .HasForeignKey(x => x.WorkspaceEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkspacePerson>()
            .HasOne(x => x.WorkspaceEntity)
            .WithMany(x => x.WorkspacePersons)
            .HasForeignKey(x => x.WorkspaceEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}