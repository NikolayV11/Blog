using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.Post.Configuration {
    public class LikeCommentConfiguration : 
        IEntityTypeConfiguration<Entity.LikeComment> {

        public void Configure ( EntityTypeBuilder<Entity.LikeComment> builder ) {
            builder.ToTable("like_comment");
            builder.HasKey(l => l.Id);

            // Связ с пользователем
            builder.HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связ с комментарием
            builder.HasOne(l => l.Commentes)
                .WithMany()
                .HasForeignKey(l => l.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            // один пользователь один лайк
            builder.HasIndex(l => new {l.UserId, l.CommentId}).IsUnique();

            // дата создания 
            builder.Property(l => l.CreatedAt).IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

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
