using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.DataAccess.Models.User.Entity;

namespace Blog.DataAccess.Models.User.Configuration {
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription> {
        public void Configure ( EntityTypeBuilder<Subscription> builder ) {
            builder.ToTable("subscriptions");
            builder.HasKey(s => s.Id);

            // Уникальный индекс: нельзя подписаться на одного и того же дважды
            builder.HasIndex(s => new { s.FollowerId, s.FollowingId }).IsUnique();

            // 1. Связь для того, КТО подписывается
            builder.HasOne(s => s.Follower)
                
                .WithMany(u => u.Following) // Ссылаемся на список "Following" у юзера
                .HasForeignKey(s => s.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Связь для того, НА КОГО подписываются
            builder.HasOne(s => s.Following)
                .WithMany(u => u.Followers) // Ссылаемся на список "Followers" у юзера
                .HasForeignKey(s => s.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

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
