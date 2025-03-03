using CritterWebApi.Entities.Post;
using CritterWebApi.Entities.User;

namespace CritterWebApi.Entities.Repost
{
    public class RepostEntity
    {
        public long UserId { get; set; }
        public UserEntity User { get; set; }

        public long PostId { get; set; }
        public PostEntity Post { get; set; }

        public string Quote { get; set; }
        public DateTime RepostedAt { get; set; } = DateTime.UtcNow;
    }
}
