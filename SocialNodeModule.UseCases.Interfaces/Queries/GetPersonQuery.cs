using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses.Person;

namespace SocialNodeModule.UseCases.Interfaces.Queries;

public record GetPersonQuery(Guid PersonId, Guid UserId) : IRequest<PersonDto>;