﻿using CritterWebApi.Services.ContextAccess;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CritterWebApi.Data.Commands.User.Create;
using CritterWebApi.Data.Inputs.User;
using CritterWebApi.Data.Queries.User.GetUser;

namespace CritterWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IContextAccess _contextAccess;

        public UserController(IMediator mediator, IContextAccess contextAccess)
        {
            _mediator = mediator;
            _contextAccess = contextAccess;
        }

        [HttpGet]
        [Authorize]
        [Route("{userId:long}")]
        public async Task<IResult> GetUser()
        {
            var result = await _mediator.Send(new GetUserQuery { UserId = _contextAccess.UserId });
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
