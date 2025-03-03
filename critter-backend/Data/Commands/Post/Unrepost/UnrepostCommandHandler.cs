using MediatR;
using CritterWebApi.Data.Repositories.PostRepository;

namespace CritterWebApi.Data.Commands.Post.Unrepost
{
    public class UnrepostCommandHandler : IRequestHandler<UnrepostCommand>
    {
        private readonly IPostRepository _postRepository;

        public UnrepostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task Handle(UnrepostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.Unrepost(request.UserId, request.PostId);
        }
    }
}
