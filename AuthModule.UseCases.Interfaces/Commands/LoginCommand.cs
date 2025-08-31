using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using Shared.Domain;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace AuthModule.UseCases.Interfaces.Commands;

public record LoginCommand(SocialNetwork Type, InitDataDto InitData): IRequest<AuthResponse>;