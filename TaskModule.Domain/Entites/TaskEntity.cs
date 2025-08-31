using System.ComponentModel.DataAnnotations;
using Shared.Domain;
using TaskModule.Domain.Enums;

namespace TaskModule.Domain.Entites;

public class TaskEntity : CreatableEntity
{
    private TaskEntity() { }
    
    public TaskEntity(string name, string? description, DateTime startDate, DateTime endDate, Guid socialNodeId,
        Guid userId, Guid workspaceId)
    {
        Name = name;
        Description = description;
        CreateDate = startDate;
        EndDate = endDate;
        SocialNodeId = socialNodeId;
        CreatorId = userId;
        WorkspaceId = workspaceId;
    }

    [Required] public string Name { get; set; }

    public string? Description { get; set; }

    [Required] public DateTime CreateDate { get; init; }

    [Required] public DateTime EndDate { get; set; }

    [Required] public StatusOfTask Status { get; set; } = StatusOfTask.Created;

    [Required] public Guid SocialNodeId { get; set; }

    [Required] public Guid WorkspaceId { get; init; }

    public void ChangeStatusTo(StatusOfTask status) => Status = status;

    public void UpdateInfo(string name, string? description, DateTime endDate, Guid socialNodeId)
    {
        Name = name;
        Description = description;
        EndDate = endDate;
        SocialNodeId = socialNodeId;
    }
}