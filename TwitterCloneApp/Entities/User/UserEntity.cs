using Microsoft.EntityFrameworkCore;
using TwitterCloneApp.Entities.Post;
using TwitterCloneApp.Entities.Repost;

namespace TwitterCloneApp.Entities.User
{
    [EntityTypeConfiguration(typeof(UserEntityConfiguration))]

    public class UserEntity
    {
        public long Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        // One-to-Many
        public List<PostEntity> Posts { get; set; } = new();

        // Many-to-Many (Likes)
        public List<PostEntity> LikedPosts { get; set; } = new();

        // Many-to-Many (Reposts)
        public List<PostEntity> RepostedPosts { get; set; } = new();

        public UserEntity(string firstName, string lastName, string email, string password, string userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            UserName = userName;
        }

        public void AddPost(PostEntity post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));

            Posts.Add(post);
        }

        //public void AddRepost(PostEntity post)
        //{
        //    if (post == null) throw new ArgumentNullException(nameof(post));

        //    Reposts.Add(post);
        //}

        public void AddLikedPost(PostEntity likedPost)
        {
            if (likedPost == null) throw new ArgumentNullException(nameof(likedPost));

            LikedPosts.Add(likedPost);
        }

        public void EditUsername(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));    
        }
    }
}
