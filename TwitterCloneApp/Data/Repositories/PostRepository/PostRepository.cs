using Microsoft.EntityFrameworkCore;
using TwitterCloneApp.Contexts;
using TwitterCloneApp.Entities.Post;

namespace TwitterCloneApp.Data.Repositories.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly TwitterCloneEFContext _context;

        public PostRepository(TwitterCloneEFContext context)
        {
            _context = context;
        }

        public async Task Create(long userId, string body)
        {
            using var context = _context;

            await context.Posts.AddAsync(new PostEntity(body, userId));

            await context.SaveChangesAsync();
        }

        public async Task Edit(long userId, long postId, string body)
        {
            using var context = _context;

            var post = await context.Posts.FindAsync(postId);

            if (post == null) throw new Exception("Cannot find post to edit");

            if (post.UserId != userId) throw new Exception("You cannot edit this post.");

            post.EditPost(body);

            await context.SaveChangesAsync();
        }

        public async Task Delete(long userId, long postId)
        {
            using var context = _context;

            var post = context.Posts.Find(postId);
            if (post.UserId != userId) throw new Exception("You cannot delete this post");

            if (post == null) throw new Exception($"Post {postId} does not exist");

            if (post.isReply)
            {
                post.ParentPost.Replies.Remove(post);
            } else
            {
                context.Posts.Remove(post);
            }

            await context.SaveChangesAsync();
        }

        public async Task LikePost(long userId, long postId)
        {
            using var context = _context;

            var user = await context.Users.FindAsync(userId);
            var post = await context.Posts.FindAsync(postId);

            if (user != null && post != null)
            {
                user.LikedPosts.Add(post);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UnlikePost(long userId, long postId)
        {
            using var context = _context;

            var post = await context.Posts
                .Include(p => p.LikedByUsers)
                .FirstOrDefaultAsync(p => p.Id == postId);


            var user = await context.Users.FindAsync(userId);

            context.Entry(post).Collection(p => p.LikedByUsers).Load();
            post.LikedByUsers.Remove(user);

            await context.SaveChangesAsync();
        }

        public async Task Repost(long userId, long postId)
        {
            using var context = _context;

            var user = await context.Users.FindAsync(userId);
            var post = await context.Posts.FindAsync(postId);

            if (user != null && post != null)
            {
                user.RepostedPosts.Add(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Unrepost(long userId, long postId)
        {
            using var context = _context;

            var post = await context.Posts
                .Include(p => p.RepostedByUsers)
                .FirstOrDefaultAsync(p => p.Id == postId);

            var user = await context.Users.FindAsync(userId);


            context.Entry(post).Collection(p => p.RepostedByUsers).Load();
            post.LikedByUsers.Remove(user);

            await context.SaveChangesAsync();
        }

        public async Task ReplyToPost(long userId, long parentPostId, string body)
        {
            using var context = _context;
            var parentPost = await context.Posts.FindAsync(parentPostId);

            if (parentPost == null)
            {
                throw new Exception("Parent post not found");
            }

            var reply = new PostEntity(body, userId);

            reply.ParentPostId = parentPostId;
            reply.SetIsReply(true);

            await context.Posts.AddAsync(reply);
            await context.SaveChangesAsync();
        }
    }
}
