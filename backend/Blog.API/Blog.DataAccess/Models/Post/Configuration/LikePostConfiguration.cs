using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.Post.Configuration {
    public class LikePostConfiguration : IEntityTypeConfiguration<Entity.LikePost> {
        public void Configure(EntityTypeBuilder<Entity.LikePost> builder ) {
            builder.ToTable("like_post");
            builder.HasKey(l =>  l.Id);

            // Связ с пользователем
            builder.HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связ с постом
            builder.HasOne(l => l.Post)
                .WithMany()
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Cascade);
                        
            // один пользователь один лайк
            builder.HasIndex(l => new {l.UserId, l.PostId}).IsUnique();

            //дата создания
            builder.Property(c => c.CreatedAt)
              .IsRequired()
              .HasColumnType("datetime")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // дата изменения
            builder.Property(c => c.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("datetime")
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

        }
    }
}
