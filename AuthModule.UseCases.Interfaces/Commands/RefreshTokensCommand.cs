using AuthModule.UseCases.Interfaces.Responses;
using MediatR;

namespace AuthModule.UseCases.Interfaces.Commands;

public record RefreshTokensCommand(TokensDto Tokens) : IRequest<TokensDto>;