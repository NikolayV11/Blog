using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.Post.Configuration {
    internal class PostImagesConfiguration : IEntityTypeConfiguration<Entity.PostImages> {
        public void Configure ( EntityTypeBuilder<Entity.PostImages> builder ) {
            builder.ToTable("post_images");
            builder.HasKey(i => i.Id);

            // сгенерированное имя
            builder.Property(i => i.StoredName)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(i => i.StoredName).IsUnique();

            // Связ с постом (Один пост => много картинок)
            builder.HasOne(i => i.Post)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PostId)
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
