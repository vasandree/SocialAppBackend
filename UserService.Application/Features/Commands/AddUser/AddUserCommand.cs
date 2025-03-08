using MediatR;
using UserService.Application.Dtos.Telegram;
using UserService.Domain;

namespace UserService.Application.Features.Commands.AddUser;

public record AddUserCommand(TelegramUser InitData): IRequest<User>;