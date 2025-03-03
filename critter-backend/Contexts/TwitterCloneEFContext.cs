using Microsoft.EntityFrameworkCore;
using CritterWebApi.Entities.Post;
using CritterWebApi.Entities.User;

namespace CritterWebApi.Contexts
{
    public class TwitterCloneEFContext : DbContext
    {
        public TwitterCloneEFContext(DbContextOptions<TwitterCloneEFContext> options) : base(options) {}

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
