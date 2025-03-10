using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.LoginWithTelegram;

public record LoginWithTelegramCommand(InitDataDto? InitData): IRequest<TokensDto>;