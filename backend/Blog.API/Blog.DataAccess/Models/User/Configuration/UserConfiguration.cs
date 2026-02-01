
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Models.User.Configuration {
    internal class UserConfiguration : IEntityTypeConfiguration<Entity.User> {
        public void Configure ( EntityTypeBuilder<Entity.User> builder ) {
            // имя таблицы
            builder.ToTable("users");

            // id
            builder.HasKey(u => u.Id);

            // имя, фамилия, отчество
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Surname).IsRequired(false).HasMaxLength(50);

            // Email
            builder.Property(u => u.Email).IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            // телефон
            builder.Property(u => u.Phone).IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(u => u.Phone).IsUnique();

            // PasswordHash
            builder.Property(u => u.PasswordHash).IsRequired()
                .HasMaxLength(255);

            // дата рождения
            builder.Property(u => u.Birthday).IsRequired();

            //дата регистрации
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

            // для фотографии профиля
            builder.HasOne(u => u.Avatar)
                .WithOne(a => a.User)
                .HasForeignKey<Entity.User>(u => u.AvatarId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Связь с постами (Один юзер -> Много постов)
            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с комментариями
            builder.HasMany(u => u.Commentes)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с лайками постов
            builder.HasMany(u => u.LikePosts)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с лайкоми комментарияв
            builder.HasMany(u => u.LikeComments)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
