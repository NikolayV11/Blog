using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Base {
    // только для изменений
    public abstract class BaseCommandRepository<TEntity> 
        : ICreateRepository<TEntity>, 
        IDeleteRepository<TEntity>, 
        IUpDateService<TEntity>
        where TEntity : class {
        protected readonly BlogDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        
        protected BaseCommandRepository(BlogDbContext context ) {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<bool> Create(TEntity entity ) {
            await _dbSet.AddAsync( entity );
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete ( TEntity entity ) {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpData(TEntity entity ) {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
