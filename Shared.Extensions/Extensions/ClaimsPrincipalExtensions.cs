using System.Security.Claims;

namespace Shared.Extensions.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal? principal)
        {
            if (principal?.Identity is not { IsAuthenticated: true })
            {
                return null;
            }

            var userIdClaim = principal.FindFirst("UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            return null;
        }
    }
}