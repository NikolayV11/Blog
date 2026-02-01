using Blog.Core.Abstractions.Service.Comments;
using Blog.Core.Abstractions.Repository; // для доступа к репозиториям
using Blog.Core.Models;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.Application.Services.Comments {
    public class CommentQueryService : ICommentQueryService {
        private readonly IGetByIdRepository<DataAccess.Models.Post.Entity.Commentes> _commentRepo;
        private readonly IGetRepository<DataAccess.Models.Post.Entity.LikeComment> _likesRepo;

        public CommentQueryService ( IGetByIdRepository<Commentes> commentRepo, IGetRepository<LikeComment> likesRepo ) {
            _commentRepo = commentRepo;
            _likesRepo = likesRepo;
        }

        // Проверка: существует ли такой комментарий в базе?
        public async Task<bool> ExistsAsync ( int commentId ) {
            var comment = await _commentRepo.GetById(commentId);
            return comment != null;
        }

        // Считаем общее количество лайков для коментария
        public async Task<int> GetLikesCountAsync ( int commentId ) {
            var allLikes = await _likesRepo.Get();
            return allLikes.Count(l => l.CommentId == commentId);
        }

        // Ищем лайки конкретного пользователя под конкретным комментарием
        public async Task<LikeCommet?> GetUserLikeAsync ( int userId, int commentId ) {
            var allLikes = await _likesRepo.Get();
            var likeEntity = allLikes.FirstOrDefault(l => l.UserId == userId && l.CommentId == commentId);

            if (likeEntity == null)
                return null;

            // Маппинг: превращаем сущность БД в доменную Модель .Core
            return LikeCommet.Create(
                likeEntity.Id,
                likeEntity.UserId,
                likeEntity.CommentId,
                likeEntity.CreatedAt
                );
        }
    }
}
