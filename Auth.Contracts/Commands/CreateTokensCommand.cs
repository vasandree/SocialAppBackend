using Auth.Contracts.Responses;
using MediatR;
using User.Domain.Entities;

namespace Auth.Contracts.Commands;

public record CreateTokensCommand(ApplicationUser User): IRequest<TokensDto>;