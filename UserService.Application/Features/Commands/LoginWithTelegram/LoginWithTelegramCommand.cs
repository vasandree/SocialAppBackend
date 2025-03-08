using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.LoginCommand.LoginWithTelegram;

public record LoginWithTelegramCommand(InitDataDto? InitData): IRequest<TokensDto>;