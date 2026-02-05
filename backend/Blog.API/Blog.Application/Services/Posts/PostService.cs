using System.IO;
using Blog.Core.Abstractions.Repository;
using Blog.Contracts.DTO.Post;
using Blog.DataAccess.Models.Post.Entity;
using Blog.Core.Abstractions.Service.Posts;

namespace Blog.Application.Services.Posts {
    public class PostService : IPostService {
        private readonly ICreateRepository<Post> _createRepo;
        private readonly IDeleteRepository<Post> _deleteRepo;
        private readonly IGetByIdRepository<Post> _getByIdRepo;

        public PostService (
    ICreateRepository<Post> createRepo,
    IDeleteRepository<Post> deleteRepo,
    IGetByIdRepository<Post> getByIdRepo ) {
            _createRepo = createRepo;
            _deleteRepo = deleteRepo;
            _getByIdRepo = getByIdRepo;
        }
        public async Task<int> CreatePostAsync ( int userId, CreatePostRequest request ) {
            // Создаем сущность для БД
            var newPost = new Post {
                Content = request.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                // Инциализируем список для фото
                Images = new List<PostImages>()
            };

            // Обработка картинок
            if (request.Images != null && request.Images.Any()) {
                foreach (var file in request.Images) {

                    // 1.генерируем уникальное имя, чтобы файлы не перезаписались
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath); // Создаст папку если её нет
                    var filePath = Path.Combine(uploadPath, fileName);

                    // 2. Сохраняем физически на диск
                    using (var stream = new FileStream(filePath, FileMode.Create)) {
                        await file.CopyToAsync(stream);
                    }

                    // 3. Добавляем запиь в коллекцию поста (связ с базой)
                    newPost.Images.Add(new PostImages {
                        StoredName = fileName,
                    });
                }
                
            }

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

