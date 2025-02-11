using Microsoft.EntityFrameworkCore;
using TwitterCloneApp.Entities.Post;
using TwitterCloneApp.Entities.Repost;

namespace TwitterCloneApp.Entities.User
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.LikedPosts)
                .WithMany(p => p.LikedByUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "PostLikes",
                    j => j.HasOne<PostEntity>().WithMany().HasForeignKey("PostId"),
                    j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId")
                );

            builder.HasMany(u => u.RepostedPosts)
                .WithMany(p => p.RepostedByUsers)
                .UsingEntity<RepostEntity>(
                    j => j.HasOne(r => r.Post)
                          .WithMany()
                          .HasForeignKey(r => r.PostId),

                    j => j.HasOne(r => r.User)
                          .WithMany()
                          .HasForeignKey(r => r.UserId),

                    j => j.Property(r => r.RepostedAt)
                          .HasDefaultValueSql("CURRENT_TIMESTAMP")
                );
        }
    }
}
