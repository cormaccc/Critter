using MediatR;
using TwitterCloneApp.Data.Repositories.PostRepository;

namespace TwitterCloneApp.Data.Commands.Post.Edit
{
    public class EditPostCommandHandler : IRequestHandler<EditPostCommand>
    {
        private readonly IPostRepository _postRepository;
        public EditPostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.Edit(request.UserId, request.PostId, request.Body);
        }
    }
}
