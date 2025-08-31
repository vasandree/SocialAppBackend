using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;
using UserModule.Domain.Enums;
using UserModule.UseCases.Interfaces.Dtos.Requests;

namespace UserModule.UseCases.Interfaces.Commands;

public record UpdateUserCommand(Guid UserId, SocialNetwork SocialNetwork, InitDataDto InitData)
    : IRequest<ApplicationUser>;