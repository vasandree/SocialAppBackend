using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.Place;

public class PaginatedPlacesDto
{
    [Required] public required List<ListedBaseSocialNodeDto> Place { get; init; }

    [Required] public required Pagination Pagination { get; init; }
}