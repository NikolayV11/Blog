using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.User.Configuration {
    public class UserImagesConfiguration: IEntityTypeConfiguration<Entity.UserImages> {
        public void Configure ( EntityTypeBuilder<Entity.UserImages> builder ) {
            // имя таблицы
            builder.ToTable("images_user");

            // ключ
            builder.HasKey(i => i.Id);

            // имя от пользователя
            builder.Property(i => i.OriginalName)
                .IsRequired()
                .HasMaxLength(255);

            // сгенерированное имя
            builder.Property(i => i.StoredName)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(i => i.StoredName)
                .IsUnique(true);

            // тип изображения
            builder.Property(i => i.ContentType)
                .IsRequired()
                .HasMaxLength(20);

            // дата создания 
            builder.Property(i => i.CreatedAt).IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // для изменения
            builder.Property(i => i.UpdatedAt)
                // Генерировать при добавлении и обновлении
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql(
                "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP"
                );

            // Обратная связь с User (Many-to-One)
            // Картинка знает, чья она, но Юзер "главный" в связи 1-к-1
            builder.HasOne(i => i.User)
                // У User может быть история картинок, если захочешь
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
