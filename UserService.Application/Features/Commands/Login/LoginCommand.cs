using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.Login;

public record LoginCommand(SocialNetwork Type, InitDataDto InitData): IRequest<TokensDto>;