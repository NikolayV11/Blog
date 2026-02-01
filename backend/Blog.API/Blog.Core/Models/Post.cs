namespace Blog.Core.Models {
    public class Post {
        private Post ( int id, string? content, 
            int userId, User author, 
            DateTime createdAt, DateTime updateAt, 
            List<LikePost> likesPost, List<Comment> commentsPost, 
            List<ImagePost> imagesPost ) {
            Id = id;
            Content = content;
            UserId = userId;
            Author = author;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
            LikesPost = likesPost;
            CommentsPost = commentsPost;
            ImagesPost = imagesPost;
        }

        public int Id { get; }
        public string? Content { get; }
        public int UserId { get; }
        public User Author { get; }

        public DateTime CreatedAt { get; }
        public DateTime UpdateAt { get; }

        // коллекции like, commit, images
        public IReadOnlyCollection<LikePost> LikesPost { get; }
        public IReadOnlyCollection<Comment> CommentsPost { get; }
        public IReadOnlyCollection<ImagePost> ImagesPost { get; }

        public static Post Create( int id, string? content, 
            int userId, User author, 
            DateTime createdAt, DateTime updateAt, 
            List<LikePost> likesPost, List<Comment> commentsPost, 
            List<ImagePost> imagesPost ) {
            return new Post(id, content, 
                userId, author, 
                createdAt, updateAt, 
                likesPost, commentsPost, imagesPost );
        }
    }
}
