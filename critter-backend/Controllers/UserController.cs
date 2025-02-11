using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApp.Data.Commands.User.Create;
using TwitterCloneApp.Data.Inputs.User;
using TwitterCloneApp.Data.Queries.User.GetUser;

namespace TwitterCloneApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{userId:long}")]
        public async Task<IResult> GetUser(long userId)
        {
            var result = await _mediator.Send(new GetUserQuery { UserId = userId });
            return Results.Ok(result);
        }

        [HttpPost]
        public async Task<IResult> CreateUser(UserCreateInputDto request)
        {
            var newUserId = await _mediator.Send(new CreateUserCommand { UserInfo = request });

            return Results.Ok(newUserId);

        }

        [HttpPatch]
        [Route("{userId:long}")]
        public void UpdateUser()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{userId:long}")]
        public void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
