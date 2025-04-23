using MediatR;
using Shared.Domain;
using User.Contracts.Dtos.Requests;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Contracts.Commands;

public record AddUserCommand(SocialNetwork SocialNetwork, InitDataDto InitData): IRequest<ApplicationUser>;