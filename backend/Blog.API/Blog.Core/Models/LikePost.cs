namespace Blog.Core.Models {
    public class LikePost {
        public int Id { get; }
        public int UserId { get; }
        public int PostId { get; }

        public DateTime CreateAt { get; }

        private LikePost ( int id, int userId, int postId, DateTime createdAt ) {
            Id = id;
            UserId = userId;
            PostId = postId;
            CreateAt = createdAt;
        }

        public static LikePost  Create(int id, int userId, int postId, DateTime createdAt ) {
            return new LikePost(id, userId, postId, createdAt);
        }
    }
}
