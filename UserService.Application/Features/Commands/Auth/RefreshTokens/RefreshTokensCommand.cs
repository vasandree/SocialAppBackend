using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.Auth.RefreshTokens;

public record RefreshTokensCommand(TokensDto Tokens) : IRequest<TokensDto>;