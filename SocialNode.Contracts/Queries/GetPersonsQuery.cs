using MediatR;
using SocialNode.Contracts.Dtos.Responses.Person;

namespace SocialNode.Contracts.Queries;

public record GetPersonsQuery(Guid UserId, int Page, string? Name) : IRequest<PaginatedPersonsDto>;