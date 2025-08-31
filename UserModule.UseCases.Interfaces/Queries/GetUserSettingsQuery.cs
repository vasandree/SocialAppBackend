using MediatR;
using UserModule.UseCases.Interfaces.Dtos;

namespace UserModule.UseCases.Interfaces.Queries;

public record GetUserSettingsQuery(Guid UserId) : IRequest<UserSettingsDto>;