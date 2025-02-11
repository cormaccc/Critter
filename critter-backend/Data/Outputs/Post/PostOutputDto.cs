namespace TwitterCloneApp.Data.Outputs.Post
{
    public class PostOutputDto
    {
        public long PostId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Body { get; set; }
        public AuthorDto Author { get; set; }
        public long LikeCount { get; set; }
        public long ReplyCount { get; set; }
        public long RepostCount { get; set; }
    }
}
