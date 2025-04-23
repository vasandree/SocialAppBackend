using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Dtos;

namespace SocialNode.Contracts.Dtos.Responses.Place;

public class PaginatedPlacesDto
{
    [Required] public List<ListedBaseSocialNodeDto> Place { get; set; }

    [Required] public Pagination Pagination { get; set; }
}