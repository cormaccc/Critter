using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TwitterCloneApp.Entities.User;

namespace TwitterCloneApp.Entities.Post
{
    [EntityTypeConfiguration(typeof(PostEntityConfiguration))]
    public class PostEntity
    {
        public long Id { get; private set; }
        public long UserId { get; private set; }
        public string Body { get; private set; } = string.Empty;
        [Column(TypeName = "DATETIME")]
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool isEdited { get; private set; } = false;
        public UserEntity User { get; private set; }
        public bool isReply { get; private set; } = false;
        public long? ParentPostId { get; set; }
        public PostEntity ParentPost { get; set; }
        public List<PostEntity> Replies { get; set; } = new();
        public List<UserEntity> LikedByUsers { get; set; } = new();
        public List<UserEntity> RepostedByUsers { get; set; } = new();


        public PostEntity(string body, long userId)
        {
            Body = body;
            UserId = userId;
        }

        public void EditPost(string body)
        {
            if (body == null) throw new ArgumentNullException(nameof(body));
            this.Body = body;
            this.isEdited = true;
        }

        public void SetIsReply(bool isReply) => this.isReply = isReply;
    }
}
