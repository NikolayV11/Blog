namespace Blog.Core.Models {
    public class ImagePost {
        public int Id { get; }
        public string StoredName { get; }
        public string Type { get; }

        public int PostId { get; }
        public DateTime CreatedAt { get; }

        private ImagePost ( int id, string storedName,
            string type, int postId, DateTime createdAt ) {
            Id = id;
            StoredName = storedName;
            Type = type;
            PostId = postId;
            CreatedAt = createdAt;
        }

        public static ImagePost Create(int id, string stored, 
            string type, int postId, DateTime createdAt ) {
            return new ImagePost(id, stored, type, postId, createdAt);
        }
    }
}
