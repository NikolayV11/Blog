using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using LikePostEntity = Blog.DataAccess.Models.Post.Entity.LikePost;

namespace Blog.DataAccess.Repositories.LikePost {
    public class LikePostRepository
        : BaseCommandRepository<LikePostEntity>
        , ICreateRepository<LikePostEntity>
        , IDeleteRepository<LikePostEntity>
        , IGetRepository<LikePostEntity>
        , IGetByIdRepository<LikePostEntity>
        {

        public LikePostRepository(BlogDbContext context): base(context) { }

        public async Task<List<LikePostEntity>> Get ( ) {
            return await _context.Set<LikePostEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<LikePostEntity?> GetById ( int id ) {
            return await _context.Set<LikePostEntity>().FindAsync(id);
        }
    }
}
