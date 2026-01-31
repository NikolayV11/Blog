using Blog.DataAccess.Models.Post.Entity;
using Blog.DataAccess.Models.User.Entity;
using Microsoft.EntityFrameworkCore;
namespace Blog.DataAccess {
    public class BlogDbContext : DbContext {
        public BlogDbContext ( DbContextOptions<BlogDbContext> options )
            : base(options) {
        }

        // Регистрируем таблицы
        public DbSet<User> Users { get; set; }
        public DbSet<UserImages> UserImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImages> PostImages { get; set; }
        public DbSet<Commentes> Comments { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }
        public DbSet<LikeComment> LikeComments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder ) {
            // Эта строка — сердце твоего подхода. 
            // Она сканирует текущую сборку (Blog.DataAccess) 
            // и применяет все классы IEntityTypeConfiguration.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
