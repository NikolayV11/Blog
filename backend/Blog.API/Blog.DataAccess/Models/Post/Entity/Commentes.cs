using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.Post.Entity {
    public class Commentes: BaseEntity {

        public string Content { get; set; }
        
        // Автор
        public int UserId { get; set; }
        public User.Entity.User Author { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public List<LikeComment> Likes { get; set; } = new();
    }
}
