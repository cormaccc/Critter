using Microsoft.EntityFrameworkCore;
using TwitterCloneApp.Entities.Post;
using TwitterCloneApp.Entities.User;

namespace TwitterCloneApp.Contexts
{
    public class TwitterCloneEFContext : DbContext
    {
        public TwitterCloneEFContext(DbContextOptions<TwitterCloneEFContext> options) : base(options) {}

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
