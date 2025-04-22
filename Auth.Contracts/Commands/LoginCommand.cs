using Auth.Contracts.Responses;
using MediatR;
using User.Contracts.Dtos.Requests;
using User.Domain.Enums;

namespace Auth.Contracts.Commands;

public record LoginCommand(SocialNetwork Type, InitDataDto InitData): IRequest<TokensDto>;