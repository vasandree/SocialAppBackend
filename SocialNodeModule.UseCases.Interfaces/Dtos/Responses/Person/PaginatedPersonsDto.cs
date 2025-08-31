using Shared.Contracts.Dtos;

namespace SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

public record PaginatedPersonsDto(List<ListedBaseSocialNodeDto> Person, Pagination Pagination);