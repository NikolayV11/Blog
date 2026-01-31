using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.Post.Entity {
    public class LikeComment : BaseEntity{
        public int UserId { get; set; }
        public User.Entity.User User { get; set; }
        public int CommentId { get; set; }
    }
}
