
namespace Blog.Core.Abstractions.Service {
    public interface ILikeServices {
        Task<bool> ToggleCommentLikeAsinc ( int userId, int commitId );
    }
}
