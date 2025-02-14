using CritterWebApi.Services.ContextAccess;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApp.Data.Queries.Feed;

namespace TwitterCloneApp.Controllers
{
    [ApiController]
    [Route("Feed")]
    public class FeedController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IContextAccess _context;
        public FeedController(IMediator mediator, IContextAccess context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IResult> GetFeed(GetFeedQuery request)
        {
           if (request == null) return Results.BadRequest();
            
           var posts = await _mediator.Send(request);

           return Results.Ok(posts);
        }

    }
}
