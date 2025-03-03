using MediatR;
using CritterWebApi.Contexts;
using CritterWebApi.Data.Repositories.PostRepository;

namespace CritterWebApi.Data.Commands.Post.Like
{
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand>
    {
        private readonly IPostRepository _postRepository;
        public LikePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(LikePostCommand request, CancellationToken cancellationToken)
        {
             await _postRepository.LikePost(request.UserId, request.PostId);
        }
    }
}
