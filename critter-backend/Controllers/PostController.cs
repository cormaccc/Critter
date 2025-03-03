using CritterWebApi.Services.ContextAccess;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CritterWebApi.Data.Commands.Post.Create;
using CritterWebApi.Data.Commands.Post.Delete;
using CritterWebApi.Data.Commands.Post.Edit;
using CritterWebApi.Data.Commands.Post.Like;
using CritterWebApi.Data.Commands.Post.ReplyToPost;
using CritterWebApi.Data.Commands.Post.Repost;
using CritterWebApi.Data.Commands.Post.Unlike;
using CritterWebApi.Data.Commands.Post.Unrepost;
using CritterWebApi.Data.Inputs.Post;
using CritterWebApi.Data.Outputs.Post;
using CritterWebApi.Data.Queries.Post.GetPost;

namespace CritterWebApi.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IContextAccess _contextAccess;

        public PostController(IMediator mediator, IContextAccess contextAccess)
        {
            _mediator = mediator;
            _contextAccess = contextAccess;
        }

        [HttpPost]
        [Authorize]
        public async Task<IResult> CreatePost(PostCreateInputDto request)
        {
            await _mediator.Send(new CreatePostCommand
            {
                Body = request.Body,
                UserId = _contextAccess.UserId
            });

            return Results.Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("{postId:long}")]
        public async Task<PostOutputDto> GetPost(long postId)
        {
            var result = await _mediator.Send(new GetPostQuery { PostId = postId, UserId = _contextAccess.UserId });

            if (result == null) throw new Exception("Could not find post");

            return result;
        }


        [HttpPatch]
        [Authorize]
        [Route("{postId:long}")]
        public async Task<IResult> UpdatePost([FromBody] PostEditInputDto request)
        {
            if (request == null) return Results.BadRequest();
            await _mediator.Send(new EditPostCommand { UserId = _contextAccess.UserId, PostId = request.PostId, Body = request.Body });

            return Results.Ok("Post edited");

        }

        [HttpDelete]
        [Authorize]
        [Route("{postId:long}")]
        public async Task<IResult> DeletePost([FromBody] PostDeleteInputDto request)
        {
            if (request == null) return Results.BadRequest();

            await _mediator.Send(new DeletePostCommand { PostId = request.PostId, UserId = _contextAccess.UserId });

            return Results.Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{postId:long}/like")]
        public async Task<IResult> LikePost(long postId)
        {
            await _mediator.Send(new LikePostCommand { PostId = postId, UserId = _contextAccess.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{postId:long}/unlike")]
        public async Task<IResult> UnlikePost(long postId)
        {
            await _mediator.Send(new UnlikePostCommand { PostId = postId, UserId = _contextAccess.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{postId:long}/repost")]
        public async Task<IResult> RepostPost(long postId)
        {
            await _mediator.Send(new RepostCommand { PostId = postId, UserId = _contextAccess.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{postId:long}/unrepost")]
        public async Task<IResult> UnrepostPost(long postId)
        {
            await _mediator.Send(new UnrepostCommand { PostId = postId, UserId = _contextAccess.UserId });
            return Results.Ok("Success");
        }

        [HttpPost]
        [Authorize]
        [Route("{postId:long}/reply")]
        public async Task<IResult> Reply(ReplyInputDto request)
        {
            await _mediator.Send(new ReplyToPostCommand { UserId = _contextAccess.UserId, ParentPostId = request.ParentPostId, Body = request.Body });

            return Results.Ok("Success");
        }
    }
}
