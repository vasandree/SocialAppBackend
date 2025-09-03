using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace UserModule.UseCases.Interfaces.Queries;

public record GetUserSettingsQuery(Guid UserId) : IRequest<UserSettingsDto>;