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

            // дата создания 
            builder.Property(l => l.CreatedAt).IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // один пользователь один лайк
            builder.HasIndex(l => new {l.UserId, l.PostId}).IsUnique();

            // для изменения
            builder.Property(l => l.UpdatedAt)
                // Генерировать при добавлении и обновлении
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(
                "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP"
                );
        }
    }
}
