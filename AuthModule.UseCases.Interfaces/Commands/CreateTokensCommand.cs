using AuthModule.UseCases.Interfaces.Responses;
using MediatR;
using UserModule.Domain.Entities;

namespace AuthModule.UseCases.Interfaces.Commands;

public record CreateTokensCommand(ApplicationUser? User): IRequest<TokensDto>;