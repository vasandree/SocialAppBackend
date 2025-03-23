using AutoMapper;
using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Persistence.Repositories.SocialNetworkAccountRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Queries.GetSocialNetworkAccounts;

public class
    GetSocialNetworkAccountsCommandHandler : IRequestHandler<GetSocialNetworkAccountsCommand,
    List<SocialNetworkAccountDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ISocialNetworkAccountRepository _socialNetworkAccountRepository;

    public GetSocialNetworkAccountsCommandHandler(ISocialNetworkAccountRepository socialNetworkAccountRepository,
        IUserRepository userRepository, IMapper mapper)
    {
        _socialNetworkAccountRepository = socialNetworkAccountRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<SocialNetworkAccountDto>> Handle(GetSocialNetworkAccountsCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfUserExistsByIdAsync(request.UserId))
            throw new BadRequest("User does not exist");

        var accounts = await _socialNetworkAccountRepository.GetAllByUserId(request.UserId);

        return _mapper.Map(accounts, new List<SocialNetworkAccountDto>());
    }
}