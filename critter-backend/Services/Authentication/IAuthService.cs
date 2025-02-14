using CritterWebApi.Data.Models;

namespace CritterWebApi.Services.Authentication
{
    public interface IAuthService
    {
        public Task<HttpAuthCookie?> Authenticate(string username, string password, bool rememberMe);
        public Task<bool> IsValidUser(string username, string password);
    }
}
