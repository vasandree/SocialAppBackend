using MediatR;
using SocialNetworkAccountModule.UseCases.Interfaces.Dtos.Responses;

namespace SocialNetworkAccountModule.UseCases.Interfaces.Queries;

public record GetPersonsSocialNetworkAccountsQuery(Guid UserId, Guid PersonId): IRequest<List<SocialNetworkAccountDto>>;