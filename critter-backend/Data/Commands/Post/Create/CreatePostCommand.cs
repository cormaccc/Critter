using MediatR;

namespace CritterWebApi.Data.Commands.Post.Create
{
    public class CreatePostCommand : IRequest
    {
        public long UserId { get; set; }
        public string Body {  get; set; }
    }
}
