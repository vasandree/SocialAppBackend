using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetPersonsQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPersonsDto>;