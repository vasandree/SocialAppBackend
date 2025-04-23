using System.ComponentModel.DataAnnotations;

namespace Workspace.Domain.Entities;

public class Person 
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
}