using MediatR;
using TwitterCloneApp.Data.Repositories.PostRepository;

namespace TwitterCloneApp.Data.Commands.Post.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IPostRepository _postRepository;
        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.Delete(request.UserId, request.PostId);
        }
    }
}
