using AutoMapper;
using MediatR;
using User.Contracts.Commands;
using User.Contracts.Dtos;
using User.Contracts.Repositories;

namespace User.Application.Features.Commands;

public class EditUserSettingsCommandHandler : IRequestHandler<EditUserSettingsCommand, UserSettingsDto>
{
    private readonly IMapper _mapper;
    private readonly IUserSettingsRepository _userSettingsRepository;


    public EditUserSettingsCommandHandler(IMapper mapper, IUserSettingsRepository userSettingsRepository)
    {
        _mapper = mapper;
        _userSettingsRepository = userSettingsRepository;
    }

    public async Task<UserSettingsDto> Handle(EditUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await _userSettingsRepository.GetByUserIdAsync(request.UserId);

        settings.Language = request.Settings.LanguageCode;
        settings.Theme = request.Settings.Theme;

        await _userSettingsRepository.UpdateAsync(settings);

        return _mapper.Map<UserSettingsDto>(settings);
    }
}