using AutoMapper;
using MediatR;
using User.Contracts.Dtos;
using User.Contracts.Queries;
using User.Contracts.Repositories;

namespace User.Application.Features.Queries;

public class GetUserSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, UserSettingsDto>
{
    private readonly IMapper _mapper;
    private readonly IUserSettingsRepository _userSettingsRepository;

    public GetUserSettingsQueryHandler(IMapper mapper, IUserSettingsRepository userSettingsRepository)
    {
        _mapper = mapper;
        _userSettingsRepository = userSettingsRepository;
    }

    public async Task<UserSettingsDto> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<UserSettingsDto>(
            await _userSettingsRepository.GetByUserIdAsync(request.UserId));
    }
}