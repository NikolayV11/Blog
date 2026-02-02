
namespace Blog.Core.Abstractions.Service.LikesPosts {
    public interface IPostLikeService {
        Task<bool> TogglePostLikeAsync ( int userId, int postId );
    }
}
