using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;

namespace TaskModule.UseCases.Interfaces.Dtos.Responses;

public record ListedTaskDto(Guid Id, string Name, DateTime EndDate)
{
    public ListedBaseSocialNodeDto SocialNode { get; set; }
}