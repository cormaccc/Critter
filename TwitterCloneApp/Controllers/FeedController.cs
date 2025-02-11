using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApp.Data.Queries.Feed;

namespace TwitterCloneApp.Controllers
{
    [ApiController]
    [Route("Feed")]
    public class FeedController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FeedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> GetFeed(GetFeedQuery request)
        {
            if (request == null) return Results.BadRequest();
            
           var posts = await _mediator.Send(request);

            return Results.Ok(posts);
        }

    }
}
