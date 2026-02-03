using Blog.Core.Abstractions.Service.Posts;
using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;
using Blog.Core.Abstractions.Service.LikesPosts;

namespace Blog.Application.Services.Posts {
    public class PostLikeQueryService
        : ILikePostQueryService
        {
        private readonly IGetRepository<LikePost> _likeRepo;

        public PostLikeQueryService(
            IGetRepository<LikePost> likeRepo ) {
            _likeRepo = likeRepo;
        }

        // общее количесто лайков у поста
        public async Task<int> GetLikesCountAsync ( int postid ) {
            var allLikes = await _likeRepo.Get();
            return allLikes.Count(l => l.PostId == postid);
        }

        // Проверка ставил ли я лайк
        public async Task<bool> IsLikedByUserAsync ( int userId, int postId ) {
            var allLikes = await _likeRepo.Get();
            return allLikes.Any(
                l => l.UserId == userId 
                && l.PostId == postId);
        }
    }
}
