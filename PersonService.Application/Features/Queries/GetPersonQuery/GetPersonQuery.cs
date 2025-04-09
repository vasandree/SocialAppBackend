using MediatR;
using PersonService.Application.Dtos.Responses.Person;

namespace PersonService.Application.Features.Queries.GetPersonQuery;

public record GetPersonQuery(Guid PersonId, Guid UserId) : IRequest<PersonDto>;