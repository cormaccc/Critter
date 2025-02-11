using MediatR;

namespace TwitterCloneApp.Data.Commands.Post.ReplyToPost
{
    public class ReplyToPostCommand : IRequest
    {
        public long UserId {  get; set; }
        public long ParentPostId { get; set; }
        public string Body { get; set; }
    }
}
