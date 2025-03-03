using MediatR;
using CritterWebApi.Data.Outputs.Post;

namespace CritterWebApi.Data.Queries.Feed
{
    public class GetFeedQuery : IRequest<IEnumerable<PostOutputDto>>
    {
        public long UserId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
    }
}
