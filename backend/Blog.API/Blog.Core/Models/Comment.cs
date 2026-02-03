namespace Blog.Core.Models {
    public class Comment {
        public int Id { get; }

        public string Content { get; }

        public int UserId { get; }
        public int PostId { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public IReadOnlyCollection<LikeCommet> Likes { get; }

        private Comment(int id, string content, 
            int userId, int postId, 
            DateTime createdAt, DateTime updatedAt,
            List<LikeCommet> likes) {
            Id = id;
            Content = content;
            UserId = userId;
            PostId = postId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Likes = likes;
        }

        public static Comment Create(int id, string content, 
            int userId, int postId, 
            DateTime createdAt, DateTime updatedAt,
            List<LikeCommet> likes) {

            return new Comment(id, content, userId, postId, createdAt, updatedAt, likes);
        }
    }
}
