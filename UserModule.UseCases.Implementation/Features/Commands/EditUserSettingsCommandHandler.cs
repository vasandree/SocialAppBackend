using AuthModule.UseCases.Interfaces.Dtos.Responses;
using AutoMapper;
using MediatR;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands;

internal sealed class EditUserSettingsCommandHandler(IMapper mapper, IUserSettingsRepository userSettingsRepository)
    : IRequestHandler<EditUserSettingsCommand, UserSettingsDto>
{
    public async Task<UserSettingsDto> Handle(EditUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await userSettingsRepository.GetByUserIdAsync(request.UserId);

        // Сохраняем текущий TimeZoneId, т.к. DTO его пока не содержит
        settings.UpdateSettings(
            request.Settings.LanguageCode,
            request.Settings.Theme,
            request.Settings.TaskReminders,
            request.Settings.EventReminders,
            settings.TimeZoneId);

        await userSettingsRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserSettingsDto>(settings);
    }
}