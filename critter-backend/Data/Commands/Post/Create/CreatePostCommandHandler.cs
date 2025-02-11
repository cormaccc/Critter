using MediatR;
using TwitterCloneApp.Data.Repositories.PostRepository;

namespace TwitterCloneApp.Data.Commands.Post.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
           await _postRepository.Create(request.UserId, request.Body);
        }
    }
}
