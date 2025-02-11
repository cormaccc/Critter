namespace TwitterCloneApp.Data.Inputs.Post
{
    public class ReplyInputDto
    {
        public long UserId { get; set; }
        public long ParentPostId { get; set; }
        public string Body { get; set; }
    }
}
