using CritterWebApi.Services.ContextAccess;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CritterWebApi.Data.Inputs.Feed;
using CritterWebApi.Data.Outputs.Post;
using CritterWebApi.Data.Queries.Feed;

namespace CritterWebApi.Controllers
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
        public async Task<IEnumerable<PostOutputDto>> GetFeed(GetFeedInputDto request)
        {
            if (request == null) throw new Exception("Invalid Feed request");
            
           return await _mediator.Send(new GetFeedQuery { Take = request.Take, Skip = request.Skip, PageIndex = request.PageIndex, UserId = _context.UserId});
        }

    }
}
