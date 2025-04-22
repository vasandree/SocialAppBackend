using MediatR;
using User.Contracts.Dtos.Requests;
using User.Domain.Entities;

namespace User.Contracts.Commands;

public record UpdateTelegramUserCommand(InitDataDto InitData): IRequest<ApplicationUser>;