using MediatR;
using TwitterCloneApp.Data.Inputs.Auth;

namespace TwitterCloneApp.Data.Queries.Auth.Login
{
    public class LoginQuery : IRequest<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
