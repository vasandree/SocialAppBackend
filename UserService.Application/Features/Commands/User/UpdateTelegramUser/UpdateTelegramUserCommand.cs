using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.User.UpdateTelegramUser;

public record UpdateTelegramUserCommand(InitDataDto InitData): IRequest<Domain.Entities.User>;