using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CritterWebApi.Entities.Repost
{
    public class RepostEntityConfiguration : IEntityTypeConfiguration<RepostEntity>
    {
        public void Configure(EntityTypeBuilder<RepostEntity> builder)
        {
            builder.HasKey(r => new { r.UserId, r.PostId });

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Post)
                .WithMany()
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(r => r.RepostedAt)
                .HasDefaultValue("CURRENT_TIMESTAMP");

            builder.Property(r => r.Quote).HasMaxLength(255);
        }
    }
}
