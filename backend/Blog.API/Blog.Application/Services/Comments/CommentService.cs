using Blog.Core.Abstractions.Service.Comments;
using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.Application.Services.Comments {
    public class CommentService : ICommentService {
        private readonly ICreateRepository<Commentes> _createRepo;
        private readonly IDeleteRepository<Commentes> _deleteRepo;
        private readonly IGetByIdRepository<Commentes> _getByIdRepo;

        public CommentService ( 
            ICreateRepository<Commentes> createRepo, 
            IDeleteRepository<Commentes> deleteRepo, 
            IGetByIdRepository<Commentes> getByIdRepo ) {
            _createRepo = createRepo;
            _deleteRepo = deleteRepo;
            _getByIdRepo = getByIdRepo;
        }

        // Создание комментария
        public async Task<int> CreateCommentAsync ( int userId, int postId, string content ) {
            var comment = new Commentes {
                UserId = userId,
                PostId = postId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            var success = await _createRepo.Create(comment);
            return success ? comment.Id : 0;
        }

        public async Task<bool> DeleteCommentAsync(int userId, int commentId ) {
            var comment = await _getByIdRepo.GetById(commentId);
            if(comment == null || comment.UserId != userId) return false;

            return await _deleteRepo.Delete(comment);
        }
    }
}
