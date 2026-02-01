using Blog.DataAccess.Models.Common;
namespace Blog.DataAccess.Models.Post.Entity {
    public class Post : BaseEntity {
        public string? Content { get; set; }
        // связь с пользователем 
        public int UserId { get; set; }
        public User.Entity.User Author { get; set; } = null!;

        // коллекция изображений (Галерея)
        public List<PostImages> Images { get; set; } = new();
        // коллекция комментариев
        public List<Commentes> Commentes { get; set; } = new();
        // коллекция лайков
        public List<LikePost> Likes { get; set; } = new();
    }
}
