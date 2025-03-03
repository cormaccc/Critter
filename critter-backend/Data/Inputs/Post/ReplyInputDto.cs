namespace CritterWebApi.Data.Inputs.Post
{
    public class ReplyInputDto
    {
        public long ParentPostId { get; set; }
        public string Body { get; set; }
    }
}
