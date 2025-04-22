using System.ComponentModel.DataAnnotations;

namespace WorkspaceService.Domain.Entities;

public class Person 
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
}