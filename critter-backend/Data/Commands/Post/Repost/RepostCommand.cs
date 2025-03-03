using MediatR;

namespace CritterWebApi.Data.Commands.Post.Repost
{
    public class RepostCommand : IRequest
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
