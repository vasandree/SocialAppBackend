using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using UserModule.Domain.Entities;

namespace AuthModule.UseCases.Interfaces.Commands;

public record CreateTokensCommand(ApplicationUser? User): IRequest<TokensDto>;