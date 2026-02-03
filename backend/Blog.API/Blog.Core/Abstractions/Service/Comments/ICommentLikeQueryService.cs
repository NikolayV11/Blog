
namespace Blog.Core.Abstractions.Service.Comments {
    public interface ICommentLikeQueryService {
        Task<int> GetLikesCountAsync ( int commitId );
        Task<bool> IsLikedByUserAsync(int userId, int commitId);
    }
}
