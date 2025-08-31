using AutoMapper;
using MediatR;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Commands;
using UserModule.UseCases.Interfaces.Dtos;

namespace UserModule.UseCases.Implementation.Features.Commands;

internal sealed class EditUserSettingsCommandHandler(IMapper mapper, IUserSettingsRepository userSettingsRepository)
    : IRequestHandler<EditUserSettingsCommand, UserSettingsDto>
{
    public async Task<UserSettingsDto> Handle(EditUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await userSettingsRepository.GetByUserIdAsync(request.UserId);

        settings.UpdateSettings(request.Settings.LanguageCode, request.Settings.Theme);

        await userSettingsRepository.SaveChangesAsync(cancellationToken);
        
        return mapper.Map<UserSettingsDto>(settings);
    }
}