using AutoMapper;
using MediatR;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Dtos;
using UserModule.UseCases.Interfaces.Queries;

namespace UserModule.UseCases.Implementation.Features.Queries;

internal sealed class GetUserSettingsQueryHandler(IMapper mapper, IUserSettingsRepository userSettingsRepository)
    : IRequestHandler<GetUserSettingsQuery, UserSettingsDto>
{
    public async Task<UserSettingsDto> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<UserSettingsDto>(
            await userSettingsRepository.GetByUserIdAsync(request.UserId));
    }
}