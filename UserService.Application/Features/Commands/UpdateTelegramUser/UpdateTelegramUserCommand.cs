using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Commands.UpdateTelegramUser;

public record UpdateTelegramUserCommand(InitDataDto InitData): IRequest<User>;