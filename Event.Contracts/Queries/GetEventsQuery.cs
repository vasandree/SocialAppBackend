using Event.Contracts.Dtos.Responses;
using MediatR;

namespace Event.Contracts.Queries;

public record GetEventsQuery(Guid UserId) : IRequest<IReadOnlyList<ListedEventDto>>;