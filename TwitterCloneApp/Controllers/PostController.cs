﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneApp.Data.Commands.Post.Create;
using TwitterCloneApp.Data.Commands.Post.Delete;
using TwitterCloneApp.Data.Commands.Post.Edit;
using TwitterCloneApp.Data.Commands.Post.Like;
using TwitterCloneApp.Data.Commands.Post.ReplyToPost;
using TwitterCloneApp.Data.Commands.Post.Repost;
using TwitterCloneApp.Data.Commands.Post.Unlike;
using TwitterCloneApp.Data.Commands.Post.Unrepost;
using TwitterCloneApp.Data.Inputs.Post;
using TwitterCloneApp.Data.Queries.Post.GetPost;

namespace TwitterCloneApp.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> CreatePost(PostCreateInputDto request)
        {
            await _mediator.Send(new CreatePostCommand { Body = request.Body, UserId = request.UserId });

            return Results.Ok();
        }

        [HttpGet]
        [Route("{postId:long}")]
        public async Task<IResult> GetPost(long postId)
        {
            var result = await _mediator.Send(new GetPostQuery { Id = postId });

            return Results.Ok(result);
        }


        [HttpPatch]
        [Route("{postId:long}")]
        public async Task<IResult> UpdatePost([FromBody] PostEditInputDto request)
        {
            if (request == null) return Results.BadRequest();
            await _mediator.Send(new EditPostCommand { UserId = request.UserId, PostId = request.PostId, Body = request.Body });

            return Results.Ok("Post edited");

        }

        [HttpDelete]
        [Route("{postId:long}")]
        public async Task<IResult> DeletePost([FromBody] PostDeleteInputDto request)
        {
            if (request == null) return Results.BadRequest();

            await _mediator.Send(new DeletePostCommand { PostId = request.PostId, UserId = request.UserId});

            return Results.Ok();
        }

        [HttpPost]
        [Route("{postId:long}/like")]
        public async Task<IResult> LikePost([FromBody] PostActionInputDto request)
        {
            await _mediator.Send(new LikePostCommand { PostId = request.PostId, UserId = request.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Route("{postId:long}/unlike")]
        public async Task<IResult> UnlikePost([FromBody] PostActionInputDto request)
        {
            await _mediator.Send(new UnlikePostCommand { PostId = request.PostId, UserId = request.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Route("{postId:long}/repost")]
        public async Task<IResult> RepostPost([FromBody] PostActionInputDto request)
        {
            await _mediator.Send(new RepostCommand { PostId = request.PostId, UserId = request.UserId });
            return Results.Ok();
        }

        [HttpPost]
        [Route("{postId:long}/unrepost")]
        public async Task<IResult> UnrepostPost([FromBody] PostActionInputDto request)
        {
            await _mediator.Send(new UnrepostCommand { PostId = request.PostId, UserId = request.UserId });
            return Results.Ok(request);
        }

        [HttpPost]
        [Route("{postId:long}/reply")]
        public async Task<IResult> Reply(ReplyInputDto request)
        {
            await _mediator.Send(new ReplyToPostCommand { UserId = request.UserId, ParentPostId = request.ParentPostId, Body = request.Body });

            return Results.Ok();
        }
    }
}
