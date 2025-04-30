using System.ComponentModel.DataAnnotations;
using Shared.Domain;

namespace SocialNode.Domain.Entities;

public class BaseSocialNode : BaseEntity
{
    [Required] public string Name { get; set; }

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }

    [Required] public Guid CreatorId { get; set; }
}