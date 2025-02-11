namespace TwitterCloneApp.Data.Inputs.Post
{
    public class PostEditInputDto
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string Body { get; set; }
    }
}
