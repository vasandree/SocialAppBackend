using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.Person;

public class PaginatedPersonsDto
{
    [Required] public required List<ListedBaseSocialNodeDto> Person { get; init; }

    [Required] public required Pagination Pagination { get; init; }
}