using MediatR;

namespace CritterWebApi.Data.Commands.Post.Unrepost
{
    public class UnrepostCommand : IRequest
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
