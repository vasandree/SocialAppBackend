using AuthModule.UseCases.Interfaces.Dtos.Requests;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;

namespace AuthModule.UseCases.Interfaces.Commands;

public record CommonLoginCommand(InitDataDto InitDataDto) : IRequest<AuthResponse>;