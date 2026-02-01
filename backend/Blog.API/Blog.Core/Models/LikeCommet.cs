namespace Blog.Core.Models {
    public class LikeCommet {
        public int Id { get; }
        public int UserId { get; }
        public int CommitId { get; }

        // Дата поставки Like
        public DateTime CreatedAt { get; }

        private LikeCommet(
            int id, int userId, int commitId, DateTime createdAt) {
            Id = id;
            UserId = userId;
            CommitId = commitId;
            CreatedAt = createdAt;
        }

        public  static LikeCommet Create(int id, int userId, int commitId, DateTime createdAt ) {
            return new LikeCommet(id, userId, commitId, createdAt);
        }

    }
}
