using CritterWebApi.Data.Models;
using MediatR;

namespace CritterWebApi.Data.Commands.User.Login
{
    public class LoginCommand : IRequest<HttpCookie?>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
