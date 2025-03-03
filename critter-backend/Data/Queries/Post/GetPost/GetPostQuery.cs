using MediatR;
using CritterWebApi.Data.Outputs.Post;

namespace CritterWebApi.Data.Queries.Post.GetPost
{
    public class GetPostQuery : IRequest<PostOutputDto>
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
