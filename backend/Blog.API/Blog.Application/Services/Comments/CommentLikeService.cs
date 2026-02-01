
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service.Comments;
using Blog.Core.Abstractions.Service.LikesComments;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.Application.Services.Comments {
    public class CommentLikeService : ILikeServices {
        private readonly ICommentQueryService _queryService;
        private readonly ICreateRepository<LikeComment> _createRepo;
        private readonly IDeleteRepository<LikeComment> _deleteRepo;

        public CommentLikeService ( ICommentQueryService queryService, 
            ICreateRepository<LikeComment> createRepo, 
            IDeleteRepository<LikeComment> deleteRepo ) {
            _queryService = queryService;
            _createRepo = createRepo;
            _deleteRepo = deleteRepo;
        }

        public async Task<bool> ToggleAsync ( int userId, int commetId ) {
            // 1. спрашиваем у Query-сервиса, существует ли коментарий?
            if (!await _queryService.ExistsAsync(commetId))
                throw new Exception("Комментарий не найден");

            // 2. проверяем, есть ли уже лайк у этого пользователя
            var existingLike = await _queryService.GetUserLikeAsync(userId, commetId);

            if (existingLike != null) {
                // 3. Если лайк есть - удаляем его
                // Нам нужно превратить доменную модель
                // Core обратно в Entity для репозитория
                var likeEntity = new LikeComment {
                    Id = existingLike.Id,
                    UserId = existingLike.UserId,
                    CommentId = existingLike.CommitId
                };

                return await _deleteRepo.Delete(likeEntity);
            } else {
                // 4. Если лайка нет - создаём новый
                var newLikeEntity = new LikeComment {
                    UserId = userId,
                    CommentId = commetId,
                    CreatedAt = DateTime.UtcNow
                };

                return await _createRepo.Create(newLikeEntity);
            }
        }
    }
}
