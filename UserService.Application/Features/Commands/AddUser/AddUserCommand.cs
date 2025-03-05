using MediatR;
using UserService.Application.Dtos.Telegram;

namespace UserService.Application.Features.Commands.AddUser;

public record AddUserCommand(TelegramUser InitData): IRequest<Unit>;