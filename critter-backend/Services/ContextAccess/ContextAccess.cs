using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace CritterWebApi.Services.ContextAccess
{
    public class ContextAccess : IContextAccess
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public long UserId { get; set; }
        public ContextAccess(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            UserId = SetUserId();
        }
        private long SetUserId() {
            var context = _contextAccessor.HttpContext;
            long userID;
            long.TryParse(context?.User.FindFirst(ClaimTypes.Name)?.Value, out userID);

            return userID;
        }
    }
}
