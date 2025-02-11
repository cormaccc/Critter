using MediatR;

namespace TwitterCloneApp.Data.Commands.Post.Delete
{
    public class DeletePostCommand : IRequest
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}
