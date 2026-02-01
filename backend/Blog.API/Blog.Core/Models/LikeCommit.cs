namespace Blog.Core.Models {
    public class LikeCommit {
        public int Id { get; }
        public int UserId { get; }
        public int CommitId { get; }

        // Дата поставки Like
        public DateTime CreatedAt { get; }

        private LikeCommit(
            int id, int userId, int commitId, DateTime createdAt) {
            Id = id;
            UserId = userId;
            CommitId = commitId;
            CreatedAt = createdAt;
        }

        public  static LikeCommit Create(int id, int userId, int commitId, DateTime createdAt ) {
            return new LikeCommit(id, userId, commitId, createdAt);
        }

    }
}
