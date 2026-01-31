using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Blog.DataAccess.Models.User.Entity;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.DataAccess.Models.Post.Configuration {
    internal class CommenesConfiguration : IEntityTypeConfiguration<Entity.Commentes> {
        public void Configure ( EntityTypeBuilder<Entity.Commentes> builder ) {
            builder.ToTable("comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired(false)
                .HasMaxLength(5000);

            // Связь с создателем
            builder.HasOne(c => c.Author)
                .WithMany(u => u.Commentes)
                // ссылка на создателя
                .HasForeignKey(u => u.UserId)
                // при удалении пользователя удалится пост
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Commentes)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            //дата регистрации
            builder.Property(c => c.CreatedAt).IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // дата изменения
            builder.Property(c => c.UpdatedAt)
            // Генерировать при добавлении и обновлении
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(
                "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP"
                );
        }
    }
}
