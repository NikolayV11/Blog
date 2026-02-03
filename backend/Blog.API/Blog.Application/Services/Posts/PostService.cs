using Blog.Core.Abstractions.Repository;
using Blog.Contracts.DTO.Post;
using Blog.DataAccess.Models.Post.Entity;
using Blog.Core.Abstractions.Service.Posts;

namespace Blog.Application.Services.Posts {
    public class PostService : IPostService {
        private readonly ICreateRepository<Post> _createRepo;
        private readonly IDeleteRepository<Post> _deleteRepo;
        private readonly IGetByIdRepository<Post> _getByIdRepo;

        public async Task<int> CreatePostAsync ( int userId, CreatePostRequest request ) {
            // Создаем сущность для БД
            var newPost = new Post {
                Content = request.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _createRepo.Create(newPost);

            // Если успех, возвращаем Id нового поста
            return result ? newPost.Id : 0;
        }

        public async Task<bool> DeletePostAsync ( int userId, int postId ) {
            var post = await _getByIdRepo.GetById(postId);

            if (post == null)
                return false;

            // Бизнес-логика: удалять может только автор
            if (post.UserId != userId) {
                throw new Exception("У вас нет прав на удаление этого поста");
            }

            return await _deleteRepo.Delete(post);
        }
    }
}

