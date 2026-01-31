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

            // Настройка связей
            builder.HasOne(s => s.Follower)
                .WithMany(u => u.Followers) // Те, на кого я подписан
                .HasForeignKey(s => s.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Following)
                .WithMany(u => u.Followers) // Те, кто подписан на меня
                .HasForeignKey(s => s.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
