using Dapper;
using MediatR;
using TwitterCloneApp.Contexts;
using TwitterCloneApp.Data.Outputs.Post;

namespace TwitterCloneApp.Data.Queries.Feed
{
    public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, IEnumerable<PostOutputDto>>
    {
        private readonly DapperContext _context;
        public GetFeedQueryHandler(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PostOutputDto>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
        {
            var query = @"
                SELECT 
                    p.Id AS PostId,
                    p.Body,
                    p.CreatedAt AS CreatedAt,
                    COUNT(DISTINCT pl.UserId) AS LikeCount,
                    COUNT(DISTINCT r.Id) AS ReplyCount,
                    COUNT(DISTINCT rp.UserId) AS RepostCount,
                    COALESCE(ul.UserId IS NOT NULL, FALSE) AS HasLiked,
                    COALESCE(ur.UserId IS NOT NULL, FALSE) AS HasReposted,
                    u.Id AS AuthorId,
                    u.Username AS Username,
                    CONCAT(u.FirstName, ' ', u.LastName) AS Name
                FROM Posts p
                    LEFT JOIN PostLikes pl ON p.Id = pl.PostId
                    LEFT JOIN Posts r ON p.Id = r.ParentPostId
                    LEFT JOIN RepostEntity rp ON p.Id = rp.PostId
                    LEFT JOIN Users u ON p.UserId = u.Id
                    LEFT JOIN PostLikes ul ON p.Id = ul.PostId AND ul.UserId = @userId
                    LEFT JOIN RepostEntity ur ON p.Id = ur.PostId AND ur.UserId = @userId
                GROUP BY p.Id, p.Body, p.UserId
                ORDER BY p.Id DESC
                LIMIT @limit OFFSET @skip;
             ";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("limit", request.Take);
            dynamicParameters.Add("skip", request.Skip);
            dynamicParameters.Add("userId", request.UserId);

            using var context = _context.CreateSQLiteConnnection();
            var result = await context.QueryAsync<PostOutputDto, AuthorDto, PostOutputDto>(query, (post, author) =>
            {
                post.Author = author;

                return post;
            },
            param: dynamicParameters,
            splitOn: "AuthorId");

            return result;


        }
    }
}
