using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.Auth.Login;

public record LoginCommand(Domain.Enums.SocialNetwork Type, InitDataDto InitData): IRequest<TokensDto>;