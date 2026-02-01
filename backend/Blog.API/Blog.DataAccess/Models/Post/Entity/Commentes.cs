using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.Post.Entity {
    public class Commentes: BaseEntity {

        public string Content { get; set; } = string.Empty;
        
        // Автор
        public int UserId { get; set; }
        public User.Entity.User Author { get; set; } = null!;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public List<LikeComment> Likes { get; set; } = new();
    }
}
