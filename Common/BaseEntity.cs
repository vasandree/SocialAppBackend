using System.ComponentModel.DataAnnotations;

namespace Common;

public class BaseEntity
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
}