using MediatR;

namespace CritterWebApi.Data.Commands.Post.Edit
{
    public class EditPostCommand : IRequest
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Body { get; set; }
    }
}
