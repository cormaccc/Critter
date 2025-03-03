using MediatR;

namespace CritterWebApi.Data.Commands.Post.Delete
{
    public class DeletePostCommand : IRequest
    {
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}
