using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.Post.Configuration {
    internal class PostConfiguration :IEntityTypeConfiguration<Entity.Post> {
        public void Configure ( EntityTypeBuilder<Entity.Post> builder ) {
            builder.ToTable("posts");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Content)
                .IsRequired(false)
                .HasMaxLength(5000);

            // коллекция комментариев
            builder.HasMany(p => p.Commentes)
                .WithOne(c => c.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // коллекция лайков
            builder.HasMany(p => p.Likes)
                .WithOne(l => l.Post)
                .OnDelete(DeleteBehavior.Cascade);

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
