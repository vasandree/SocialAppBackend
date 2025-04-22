using Auth.Contracts.Responses;
using MediatR;
using User.Contracts.Dtos.Requests;

namespace Auth.Contracts.Commands;

public record LoginWithTelegramCommand(InitDataDto? InitData): IRequest<TokensDto>;