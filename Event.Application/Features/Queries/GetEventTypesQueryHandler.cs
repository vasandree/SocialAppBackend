using AutoMapper;
using Event.Contracts.Dtos.Responses;
using Event.Contracts.Queries;
using Event.Contracts.Repositories;
using MediatR;

namespace Event.Application.Features.Queries;

public class GetEventTypesQueryHandler : IRequestHandler<GetEventTypesQuery, List<EventTypeResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IEventTypeRepository _eventTypeRepository;

    public GetEventTypesQueryHandler(IEventTypeRepository eventTypeRepository, IMapper mapper)
    {
        _eventTypeRepository = eventTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<EventTypeResponseDto>> Handle(GetEventTypesQuery request,
        CancellationToken cancellationToken)
    {
        var eventTypes = await _eventTypeRepository.GetByCreatorIdAsync(request.UserId);

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            eventTypes = eventTypes.Where(e => e.Name.Contains(request.Name));
        }

        return _mapper.Map<List<EventTypeResponseDto>>(eventTypes.ToList());
    }
}