using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using User.Contracts.Repositories;

namespace Shared.Extensions.Extensions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UserExistsAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        try
        {
            var userId = user.GetUserId();

            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var exists = await userRepository.CheckIfExists(userId);

            if (!exists)
            {
                context.Result = new UnauthorizedResult();
            }
        }
        catch
        {
            context.Result = new UnauthorizedResult();
        }
    }
}