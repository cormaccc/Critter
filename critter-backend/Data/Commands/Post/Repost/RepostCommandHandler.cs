using MediatR;
using CritterWebApi.Data.Repositories.PostRepository;

namespace CritterWebApi.Data.Commands.Post.Repost
{
    public class RepostCommandHandler : IRequestHandler<RepostCommand>
    {
        private readonly IPostRepository _postRepository;
        public RepostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(RepostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.Repost(request.UserId, request.PostId);
        }
    }
}
