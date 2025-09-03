using AuthModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser.Factory;

internal interface IAddUserCommandFactory
{
    IRequest<ApplicationUser> Create(SocialNetwork? type, InitDataDto initData);
}