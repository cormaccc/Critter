using CritterWebApi.Data.Models;
using MediatR;

namespace CritterWebApi.Data.Commands.User.Login
{
    public class LoginCommand : IRequest<HttpAuthCookie?>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
