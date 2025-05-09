using System.ComponentModel.DataAnnotations;

namespace Shared.Domain;

public class BaseEntity
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
}