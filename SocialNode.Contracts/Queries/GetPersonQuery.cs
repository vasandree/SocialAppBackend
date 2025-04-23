using MediatR;
using SocialNode.Contracts.Dtos.Responses.Person;

namespace SocialNode.Contracts.Queries;

public record GetPersonQuery(Guid PersonId, Guid UserId) : IRequest<PersonDto>;