using CritterWebApi.Data.Models;
using TwitterCloneApp.Entities.User;

namespace CritterWebApi.Services.Authentication
{
    public interface IAuthService
    {
        public Task<HttpCookie?> Authenticate(string username, string password, bool rememberMe);
        public Task<UserEntity> IsValidUser(string username, string password);
    }
}
