using Auth.Contracts.Responses;
using MediatR;

namespace Auth.Contracts.Commands;

public record RefreshTokensCommand(TokensDto Tokens) : IRequest<TokensDto>;