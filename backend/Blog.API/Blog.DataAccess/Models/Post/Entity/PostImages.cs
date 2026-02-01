using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.Post.Entity {
    public class PostImages: BaseEntity {
        public string StoredName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        // Внешняя связка с постом
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
