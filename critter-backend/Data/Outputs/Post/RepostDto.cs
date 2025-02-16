using TwitterCloneApp.Data.Outputs.Post;

namespace CritterWebApi.Data.Outputs.Post
{
    public class RepostDto
    {
        public long ParentId { get; set; }
        public long RepostId { get; set; }
        public PostOutputDto PostOutput { get; set; }
    }
}
