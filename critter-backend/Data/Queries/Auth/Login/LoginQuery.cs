using MediatR;
using CritterWebApi.Data.Inputs.Auth;

namespace CritterWebApi.Data.Queries.Auth.Login
{
    public class LoginQuery : IRequest<long>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
