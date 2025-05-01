using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using User.Contracts.Repositories;

namespace Shared.Configurations.AuthPolicy;

public class UserExistsHandler : AuthorizationHandler<UserExistsRequirement>
{
    private readonly IUserRepository _userRepository;

    public UserExistsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        UserExistsRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            context.Fail();
            return;
        }

        if (await _userRepository.CheckIfExists(userId))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}