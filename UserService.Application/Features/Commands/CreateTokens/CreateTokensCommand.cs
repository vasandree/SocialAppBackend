using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Domain;

namespace UserService.Application.Features.Commands.CreateTokens;

public record CreateTokensCommand(User User): IRequest<TokensDto>;