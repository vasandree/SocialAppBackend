using AutoMapper;
using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Dtos.Responses;
using EventModule.UseCases.Interfaces.Queries;
using MediatR;

namespace EventModule.UseCases.Implementation.Features.Queries;

internal sealed class GetEventTypesQueryHandler(IEventTypeRepository eventTypeRepository, IMapper mapper)
    : IRequestHandler<GetEventTypesQuery, List<EventTypeResponseDto>>
{
    public Task<List<EventTypeResponseDto>> Handle(GetEventTypesQuery request,
        CancellationToken cancellationToken)
    {
        var eventTypes = eventTypeRepository.GetByCreatorIdAsync(request.UserId);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            eventTypes = eventTypes.Where(e => e.Name.Contains(request.Name));
        }

        return Task.FromResult(mapper.Map<List<EventTypeResponseDto>>(eventTypes.ToList()));
    }
}