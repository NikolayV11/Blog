using Blog.Contracts.DTO.Post;

// создание/удалениу поста
namespace Blog.Core.Abstractions.Posts {
    public interface IPostService {
        // Возвращает ID нового поста или ошибку
        Task<int> CreatePostAsync ( int userId, CreatePostRequest request );

        // Возвращает bool после удаления
        Task<bool> DeletePostAsync ( int userId, int postId );
    }
}
