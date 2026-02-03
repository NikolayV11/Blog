using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using LikeCommentEntity = Blog.DataAccess.Models.Post.Entity.LikeComment;

namespace Blog.DataAccess.Repositories.LikeComment {
    public class LikeCommentRepository
        : BaseCommandRepository<LikeCommentEntity>
        , ICreateRepository<LikeCommentEntity>
        , IDeleteRepository<LikeCommentEntity>
        , IGetRepository<LikeCommentEntity>
        , IGetByIdRepository<LikeCommentEntity>
        {

        public LikeCommentRepository(BlogDbContext context): base(context) { }

        public async Task<List<LikeCommentEntity>> Get ( ) {
            return await _context.Set<LikeCommentEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<LikeCommentEntity?> GetById ( int id ) {
            return await _context.Set<LikeCommentEntity>().FindAsync(id);
        }
    }
}
