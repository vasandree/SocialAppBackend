using MediatR;
using PersonService.Application.Dtos.Responses;
using PersonService.Domain.Enums;

namespace PersonService.Application.Features.Queries.GetAllSocialNodesQuery;

public record GetAllSocialNodesQuery(Guid UserId, int? Page, SocialNodeType? Type) : IRequest<AllSocialNodesDto>;