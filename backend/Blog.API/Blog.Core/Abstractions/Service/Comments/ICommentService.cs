namespace Blog.Core.Abstractions.Service.Comments {
    public interface ICommentService {
        // Возвращаем ID Созданного комментария
        Task<int> CreateCommentAsync ( int userId, int postId, string content );
        Task<bool> DeleteCommentAsync ( int userId, int commentId ); 
    }
}
