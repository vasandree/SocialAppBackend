using MediatR;
using PersonService.Application.Dtos.Responses;

namespace PersonService.Application.Features.Queries.GetPersonQuery;

public record GetPersonQuery(Guid PersonId) : IRequest<PersonDto>;