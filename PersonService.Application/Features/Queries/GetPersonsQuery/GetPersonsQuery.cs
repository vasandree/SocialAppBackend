using MediatR;
using PersonService.Application.Dtos.Responses.Person;

namespace PersonService.Application.Features.Queries.GetPersonsQuery;

public record GetPersonsQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPersonsDto>;