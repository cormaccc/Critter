using CritterWebApi.Data.Enums;
using CritterWebApi.Data.Outputs.Post;
using Dapper;
using MediatR;
using CritterWebApi.Contexts;
using CritterWebApi.Data.Outputs.Post;

namespace CritterWebApi.Data.Queries.Feed
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
				SELECT * FROM (
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
						u.FirstName || ' ' || u.LastName AS Name,
						NULL AS ReposterId,
						NULL AS ReposterUsername,
						NULL AS ReposterName,
						'post' AS Type
					FROM Posts p
						LEFT JOIN PostLikes pl ON p.Id = pl.PostId
						LEFT JOIN Posts r ON p.Id = r.ParentPostId
						LEFT JOIN RepostEntity rp ON p.Id = rp.PostId  -- Check if the post is a repost
						LEFT JOIN Users u ON p.UserId = u.Id
						LEFT JOIN PostLikes ul ON p.Id = ul.PostId AND ul.UserId = 3
						LEFT JOIN RepostEntity ur ON p.Id = ur.PostId AND ur.UserId = 3
					GROUP BY p.Id, p.Body, p.CreatedAt, u.Id, u.Username, u.FirstName, u.LastName, ul.UserId, ur.UserId
	    
					UNION ALL
	
					SELECT
						rp.PostId as PostId,
						p.Body as Body,
						rp.RepostedAt as CreatedAt,
						COUNT(DISTINCT ul.UserId) as LikeCount,
						COUNT(DISTINCT r.Id) AS ReplyCount,
						COUNT(DISTINCT rp.UserId) AS RepostCount,
						COALESCE(ul.UserId IS NOT NULL, FALSE) AS HasLiked,
						COALESCE(ur.UserId IS NOT NULL, FALSE) AS HasReposted,
						ou.Id AS AuthorId,
						ou.UserName AS Username,
						ou.FirstName || ' ' || ou.LastName AS Name,
						u.Id AS ReposterId,
						u.UserName AS ReposterUserName,
						u.FirstName || ' ' || u.LastName AS ReposterName,
						'repost' AS Type
		
					FROM RepostEntity rp
						LEFT JOIN Posts p ON rp.PostId = p.Id -- This gives us the post details
						LEFT JOIN Posts r ON rp.PostId = r.ParentPostId -- this gives us replies
						LEFT JOIN Users u ON rp.UserId = u.Id -- this gets the repost user
						LEFT JOIN Users ou ON p.UserId = ou.Id -- this SHOULD get the OG author
						LEFT JOIN PostLikes ul ON rp.PostId = ul.PostId AND ul.UserId = 3 -- this gets the HasLiked details
						LEFT JOIN RepostEntity ur ON rp.PostId = ur.PostId AND ur.UserId = 3 -- this gets the HasReposted details
					GROUP BY rp.PostId, p.Body, rp.RepostedAt, ou.Id, ou.Username, ou.FirstName, ou.LastName, 
						u.Id, u.Username, u.FirstName, u.LastName, ul.UserId, ur.UserId
    
				) as CombinedResults
				ORDER BY CreatedAt DESC
             ";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("limit", request.Take);
            dynamicParameters.Add("skip", request.Skip);
            dynamicParameters.Add("userId", request.UserId);

            using var context = _context.CreateSQLiteConnnection();
            var result = await context.QueryAsync<PostOutputDto, AuthorDto, RepostDto, PostOutputDto>(query, (post, author, repost) =>
            {
                post.Author = author;

                if (repost != null && repost?.ReposterId != null)
                {
					post.Type = PostType.Repost;
                    post.Repost = new RepostDto { ReposterId = repost.ReposterId, ReposterName = repost.ReposterName, ReposterUserName = repost.ReposterUserName };
                } else
				{
					post.Type = PostType.Post;
				}

                return post;
            },
            param: dynamicParameters,
            splitOn: "AuthorId,ReposterId");

            return result;


        }
    }
}
