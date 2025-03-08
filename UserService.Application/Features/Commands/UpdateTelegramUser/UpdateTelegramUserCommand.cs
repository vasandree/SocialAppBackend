using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.UpdateTelegramUser;

public record UpdateTelegramUserCommand(InitDataDto InitData): IRequest<Unit>;