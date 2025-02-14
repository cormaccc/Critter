
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace CritterWebApi.Middleware.Context
{
    public class UserContext
    {
        private readonly RequestDelegate _next;

        public UserContext(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated
            if (context.User.Identity.IsAuthenticated)
            {
                // Extract UserID from claims
                var userId = context.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    // Store it in HttpContext.Items so it's accessible in controllers
                    context.Items["UserID"] = userId;
                }
            }

            await _next(context);
        }
    }
}
