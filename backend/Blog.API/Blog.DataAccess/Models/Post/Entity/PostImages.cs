using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.Post.Entity {
    public class PostImages: BaseEntity {
        public string OriginalName { get; set; }
        public string StoredName { get; set; }
        public string ContentType { get; set; }

        // Внешняя связка с постом
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
