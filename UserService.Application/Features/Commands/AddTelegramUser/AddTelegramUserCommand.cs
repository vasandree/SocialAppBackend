using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Commands.AddTelegramUser;

public record AddTelegramUserCommand(InitDataDto InitData): IRequest<User>;