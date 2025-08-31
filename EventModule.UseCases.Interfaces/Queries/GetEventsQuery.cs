using EventModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace EventModule.UseCases.Interfaces.Queries;

public record GetEventsQuery(Guid UserId) : IRequest<IReadOnlyList<ListedEventDto>>;