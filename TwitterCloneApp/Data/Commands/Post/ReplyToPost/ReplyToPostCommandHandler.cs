﻿using MediatR;
using TwitterCloneApp.Data.Repositories.PostRepository;

namespace TwitterCloneApp.Data.Commands.Post.ReplyToPost
{
    public class ReplyToPostCommandHandler : IRequestHandler<ReplyToPostCommand>
    {
        private readonly IPostRepository _postRepository;
        public ReplyToPostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(ReplyToPostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.ReplyToPost(request.UserId, request.ParentPostId, request.Body);
        }
    }
}
