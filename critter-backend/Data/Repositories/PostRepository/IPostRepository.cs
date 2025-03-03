namespace CritterWebApi.Data.Repositories.PostRepository
{
    public interface IPostRepository
    {
        public Task Create(long userId, string body);
        public Task Edit(long userId, long postId, string body);
        public Task Delete(long userId, long postId);
        public Task LikePost(long userId, long postId);
        public Task UnlikePost(long userId, long postId);
        public Task Repost(long userId, long postId);
        public Task Unrepost(long userId, long postId);
        public Task ReplyToPost(long userId, long parentPostId, string body);
    }
}
