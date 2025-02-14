using CritterWebApi.Data.Commands.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TwitterCloneApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login(LoginCommand request)
        {
            var result = await _mediator.Send(request);

            if (result == null) return Results.Unauthorized();

            Response.Cookies.Append(result.Name, result.TokenString, result.CookieOptions);

            return Results.Ok("Login success");
        }

        [HttpGet]
        [Route("logout")]
        public void Logout() {  throw new NotImplementedException(); }
    }
}
