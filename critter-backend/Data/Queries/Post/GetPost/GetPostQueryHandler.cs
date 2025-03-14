﻿using Dapper;
using MediatR;
using System.Globalization;
using CritterWebApi.Contexts;
using CritterWebApi.Data.Outputs.Post;

namespace CritterWebApi.Data.Queries.Post.GetPost
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostOutputDto>
    {
        private readonly DapperContext _context;
        public GetPostQueryHandler(DapperContext context)
        {
            _context = context;
        }
        public async Task<PostOutputDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
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
                    CONCAT(u.FirstName,' ',u.lastName) AS Name
                FROM Posts p
                    LEFT JOIN PostLikes pl ON p.Id = pl.PostId
                    LEFT JOIN Posts r ON p.Id = r.ParentPostId
                    LEFT JOIN RepostEntity rp ON p.Id = rp.PostId
                    LEFT JOIN Users u ON p.UserId = u.Id
                    LEFT JOIN PostLikes ul ON p.Id = ul.PostId AND ul.UserId = @userId
                    LEFT JOIN RepostEntity ur ON p.Id = ur.PostId AND ur.UserId = @userId
                WHERE p.Id = @postId
                GROUP BY p.Id, p.Body, p.UserId
                ORDER BY p.Id DESC;  -- Order by latest post              
             ";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("postId", request.PostId);
            dynamicParameters.Add("userId", request.UserId);

            using var context = _context.CreateSQLiteConnnection();
            var result = await context.QueryAsync<PostOutputDto, AuthorDto, PostOutputDto>(query, (post, author) =>
            {
                post.Author = author;

                return post;
            },
            param: dynamicParameters,
            splitOn: "AuthorId");

            return result.FirstOrDefault();
        }
    }
}
