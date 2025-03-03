using CritterWebApi.Data.Enums;
using CritterWebApi.Data.Outputs.Post;

namespace CritterWebApi.Data.Outputs.Post
{
    public class PostOutputDto
    {
        public long PostId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Body { get; set; }
        public AuthorDto Author { get; set; }
        public RepostDto? Repost { get; set; }
        public bool HasLiked { get; set; }
        public bool HasReposted { get; set; }
        public long LikeCount { get; set; }
        public long ReplyCount { get; set; }
        public long RepostCount { get; set; }
        public PostType Type { get; set; }
    }
}
