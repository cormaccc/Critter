using MediatR;

namespace TwitterCloneApp.Data.Commands.Post.Like
{
    public class LikePostCommand : IRequest
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
