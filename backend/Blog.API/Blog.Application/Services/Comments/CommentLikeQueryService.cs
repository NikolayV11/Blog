using Blog.Core.Abstractions.Service.Comments;
using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.Application.Services.Comments {
    public class CommentLikeQueryService 
        : ICommentLikeQueryService
        {
        private readonly IGetRepository<LikeComment> _likeRepo;
        public CommentLikeQueryService(IGetRepository<LikeComment> likeRepo) {
            _likeRepo = likeRepo;
        }

        public async Task<int> GetLikesCountAsync ( int commetId ) {
            var all = await _likeRepo.Get();
            return all.Count(l => l.CommentId == commetId);
        }

        public async Task<bool> IsLikedByUserAsync(int userId, int commentId ) {
            var all = await _likeRepo.Get();
            return all.Any(
                l => l.UserId == userId
            && l.CommentId == commentId);
        }
    }
}
