
using Blog.Core.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Base {
    // только чтение
    public abstract class BaseQueryRepository<TEntity> 
        : IGetRepository<TEntity> where TEntity : class {
        private readonly BlogDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseQueryRepository(BlogDbContext context ) {
            _context = context;
        }

        public async Task<List<TEntity>> Get ( ) {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id ) {
            return await _dbSet.FindAsync(id);
        }
    }
}
