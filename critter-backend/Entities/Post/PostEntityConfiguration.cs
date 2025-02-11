using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterCloneApp.Entities.Repost;
using TwitterCloneApp.Entities.User;

namespace TwitterCloneApp.Entities.Post
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Body).IsRequired().HasMaxLength(255);
            builder.Property(e => e.isEdited).IsRequired();
            builder.Property(e => e.isReply).IsRequired();

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(p => p.ParentPost)
                .WithMany(p => p.Replies)
                .HasForeignKey(p => p.ParentPostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.LikedByUsers)
                .WithMany(u => u.LikedPosts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostLikes",
                    j => j.HasOne<UserEntity>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<PostEntity>().WithMany().HasForeignKey("PostId")
                );

            builder.HasMany(p => p.RepostedByUsers)
                .WithMany(u => u.RepostedPosts)
                .UsingEntity<RepostEntity>(
                    j => j.HasOne(r => r.User)
                          .WithMany()
                          .HasForeignKey(r => r.UserId),

                    j => j.HasOne(r => r.Post)
                          .WithMany()
                          .HasForeignKey(r => r.PostId),

                    j => j.Property(r => r.RepostedAt)
                          .HasDefaultValueSql("CURRENT_TIMESTAMP")
                );
        }
    }
}
