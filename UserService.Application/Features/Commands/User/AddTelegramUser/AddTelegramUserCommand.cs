using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.User.AddTelegramUser;

public record AddTelegramUserCommand(InitDataDto InitData): IRequest<Domain.Entities.User>;