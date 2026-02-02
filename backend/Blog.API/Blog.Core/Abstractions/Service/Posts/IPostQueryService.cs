using Blog.Contracts.DTO.Post;

// как будем получать посты
namespace Blog.Core.Abstractions.Service.Posts {
    public interface IPostQueryService {
        // получим все посты
        Task<List<PostResponse>> GetPostsAsync ( );

        // Получаем пост со всеми деталями
        Task<PostResponse?> GetPostByIdAsync ( int postId );

        // получаем посты определённого пользователя
        Task<List<PostResponse>> GetUserPostsAsinc ( int userId );
    }
}
