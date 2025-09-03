using AuthModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using UserModule.Domain.Entities;

namespace UserModule.UseCases.Interfaces.Commands;

public record AddTelegramUserCommand(InitDataDto InitData): IRequest<ApplicationUser>;