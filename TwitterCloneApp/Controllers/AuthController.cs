using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApp.Data.Inputs.Auth;
using TwitterCloneApp.Data.Queries.Auth.Login;

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
        public async Task<IResult> Login(LoginInputDto request)
        {
            var result = await _mediator.Send(new LoginQuery { Email = request.Email, Password = request.Password });

            if (result > 0)
            {
                return Results.Ok(result);
            } else { return Results.BadRequest("Cannot login"); }
        }

        [HttpGet]
        [Route("logout")]
        public void Logout() {  throw new NotImplementedException(); }
    }
}
