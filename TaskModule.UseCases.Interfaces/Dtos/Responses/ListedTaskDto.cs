using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public record ListedTaskDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public DateTime EndDate { get; init; }
    public ListedBaseSocialNodeDto SocialNode { get; set; }
}