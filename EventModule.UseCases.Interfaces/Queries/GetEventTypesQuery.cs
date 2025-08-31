using EventModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace EventModule.UseCases.Interfaces.Queries;

public record GetEventTypesQuery(Guid UserId, string? Name) : IRequest<List<EventTypeResponseDto>>;