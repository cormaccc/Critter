using MediatR;

namespace TwitterCloneApp.Data.Commands.Post.Create
{
    public class CreatePostCommand : IRequest
    {
        public long UserId { get; set; }
        public string Body {  get; set; }
    }
}
