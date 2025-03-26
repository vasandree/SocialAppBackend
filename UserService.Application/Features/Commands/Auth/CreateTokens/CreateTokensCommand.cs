using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.Auth.CreateTokens;

public record CreateTokensCommand(Domain.Entities.User User): IRequest<TokensDto>;