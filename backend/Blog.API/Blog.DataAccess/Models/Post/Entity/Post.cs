using Blog.DataAccess.Models.Common;
using Blog.DataAccess.Models.User.Entity;
namespace Blog.DataAccess.Models.Post.Entity {
    public class Post : BaseEntity {
        public string? Content { get; set; }
        // связь с пользователем 
        public int UserId { get; set; }
        public User.Entity.User Author { get; set; }

        // коллекция изображений (Галерея)
        public List<PostImages> Images { get; set; } = new();
    }
}
