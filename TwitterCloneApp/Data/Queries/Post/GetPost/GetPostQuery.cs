using MediatR;
using TwitterCloneApp.Data.Outputs.Post;

namespace TwitterCloneApp.Data.Queries.Post.GetPost
{
    public class GetPostQuery : IRequest<PostOutputDto>
    {
        public long Id { get; set; }
    }
}
