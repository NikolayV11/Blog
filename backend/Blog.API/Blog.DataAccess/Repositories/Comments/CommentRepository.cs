using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;
using Blog.DataAccess.Repositories.Base;

namespace Blog.DataAccess.Repositories.Comments {
    public class CommentRepository
        : BaseQueryRepository<Commentes>
        , ICreateRepository<Commentes>
        , IDeleteRepository<Commentes>
        ,IGetRepository<Commentes>
        ,IGetByIdRepository<Commentes>
        {

        public CommentRepository(BlogDbContext context)
            : base(context) { }

        public async Task<bool> Create(Commentes entity ) {
            await _dbSet.AddAsync( entity );
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete ( Commentes entity ) {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
