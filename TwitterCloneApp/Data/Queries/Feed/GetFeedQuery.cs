using MediatR;
using TwitterCloneApp.Data.Outputs.Post;

namespace TwitterCloneApp.Data.Queries.Feed
{
    public class GetFeedQuery : IRequest<IEnumerable<PostOutputDto>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
    }
}
