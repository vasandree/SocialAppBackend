using System.ComponentModel.DataAnnotations;

namespace Shared.Domain;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; protected init; } = Guid.NewGuid();
    
}