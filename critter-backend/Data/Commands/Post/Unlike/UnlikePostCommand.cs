using MediatR;

namespace CritterWebApi.Data.Commands.Post.Unlike
{
    public class UnlikePostCommand : IRequest
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
