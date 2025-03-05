using MediatR;
using UserService.Application.Dtos.Telegram;

namespace UserService.Application.Features.Commands.UpdateUser;

public record UpdateUserCommand(TelegramUser User) : IRequest<Unit>;