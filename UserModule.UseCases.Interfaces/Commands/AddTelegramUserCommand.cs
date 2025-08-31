using MediatR;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace UserModule.UseCases.Interfaces.Commands;

public record AddTelegramUserCommand(InitDataDto InitData): IRequest<ApplicationUser>;