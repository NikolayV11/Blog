using Blog.Core.Abstractions.Service.LikesPosts;
using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.Application.Services.Posts {
    public class PostLikeService 
        :IPostLikeService
        {
        private readonly ILikePostQueryService _query;
        private readonly ICreateRepository<LikePost> _createRepo;
        private readonly IDeleteRepository<LikePost> _deleteRepo;
        private readonly IGetRepository<LikePost> _getRepo;

        public PostLikeService ( 
            ILikePostQueryService query, 
            ICreateRepository<LikePost> createRepo, 
            IDeleteRepository<LikePost> deleteRepo, 
            IGetRepository<LikePost> getRepo 
            ) {
            _query = query;
            _createRepo = createRepo;
            _deleteRepo = deleteRepo;
            _getRepo = getRepo;
        }


        public async Task<bool> TogglePostLikeAsync(int userId, int postId ) {
            // узнаем есть ли лайк от пользователя
            var isLiked = await _query.IsLikedByUserAsync(userId, postId);

            // Если есть удаляем
            if (isLiked) {
                var allLike = await _getRepo.Get();

                var like = allLike.First(
                    l => l.UserId == userId
                    && l.PostId == postId);

                return await _deleteRepo.Delete(like);
            }

            // Если нет ставим
            var newLike = new LikePost {
                UserId = userId,
                PostId = postId,
                CreatedAt = DateTime.UtcNow
            };

            return await _createRepo.Create(newLike);
        }
    }
}
