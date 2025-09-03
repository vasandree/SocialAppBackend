using AuthModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;


namespace UserModule.UseCases.Interfaces.Commands;

public record UpdateUserCommand(Guid UserId, SocialNetwork SocialNetwork, InitDataDto InitData)
    : IRequest<ApplicationUser>;