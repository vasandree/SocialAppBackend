using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.RefreshTokens;

public record RefreshTokensCommand(Guid UserId, string RefreshToken) : IRequest<TokensDto>;