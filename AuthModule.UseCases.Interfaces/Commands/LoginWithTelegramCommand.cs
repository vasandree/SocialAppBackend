using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace AuthModule.UseCases.Interfaces.Commands;

public record LoginWithTelegramCommand(InitDataDto? InitData): IRequest<AuthResponse>;