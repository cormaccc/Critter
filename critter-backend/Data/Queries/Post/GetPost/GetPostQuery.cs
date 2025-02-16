using MediatR;
using TwitterCloneApp.Data.Outputs.Post;

namespace TwitterCloneApp.Data.Queries.Post.GetPost
{
    public class GetPostQuery : IRequest<PostOutputDto>
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
