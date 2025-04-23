using Auth.Contracts.Responses;
using MediatR;
using Shared.Domain;
using User.Contracts.Dtos.Requests;

namespace Auth.Contracts.Commands;

public record LoginCommand(SocialNetwork Type, InitDataDto InitData): IRequest<TokensDto>;