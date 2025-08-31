using Shared.Contracts.Dtos;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Place;

public record PaginatedPlacesDto(List<ListedBaseSocialNodeDto> Place, Pagination Pagination);