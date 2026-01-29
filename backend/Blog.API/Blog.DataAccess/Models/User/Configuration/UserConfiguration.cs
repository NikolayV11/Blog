
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Entites.User.Configuration {
    internal class UserConfiguration : IEntityTypeConfiguration<Entity.User> {
        public void Configure ( EntityTypeBuilder<Entity.User> builder ) {
            // имя таблицы
            builder.ToTable("users");

            // id
            builder.HasKey(u => u.Id);

            // имя, фамилия, отчество
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Surname).HasMaxLength(50);

            // Email
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            // телефон
            builder.Property(u => u.Phone).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.Phone).IsUnique();

            // PasswordHash
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);

            // дата рождения
            builder.Property(u => u.Birthday).IsRequired();

            //дата регистрации
            builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

            // дата изменения
            builder.Property(u => u.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate() // Генерировать при добавлении и обновлении
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
            ;
        }
    }
}
