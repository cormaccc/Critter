using MediatR;
using TwitterCloneApp.Data.Repositories.PostRepository;

namespace TwitterCloneApp.Data.Commands.Post.Unlike
{
    public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public UnlikePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(UnlikePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.UnlikePost(request.UserId, request.PostId);
        }
    }
}
