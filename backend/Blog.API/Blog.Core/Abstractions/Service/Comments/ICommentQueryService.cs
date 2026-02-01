using Blog.Core.Models;

namespace Blog.Core.Abstractions.Service.Comments {
    public interface ICommentQueryService {
        Task<bool> ExistsAsync ( int commentId );
        Task<int> GetLikesCountAsync( int commentId );
        Task<LikeCommet?> GetUserLikeAsync ( int userId, int commentId );
    }
}
