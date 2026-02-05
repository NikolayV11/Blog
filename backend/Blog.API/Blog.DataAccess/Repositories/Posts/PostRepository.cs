using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.Post.Entity;
using Blog.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Posts {
    // достаем посты и сразу добовляем автора
    public class PostRepository : 
        BaseQueryRepository<Post>,
        ICreateRepository<Post>,
        IDeleteRepository<Post>,
        IGetRepository<Post>
        { 

        public PostRepository(BlogDbContext context) : base(context) { }

        // Переобределяем стандартный Get, чтобы подтянуть связи
        public new async Task<List<Post>> Get ( ) {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Images)
                .Include(p => p.Commentes)
                .Include(p => p.Likes)
                .ToListAsync();
        }

        // Создание поста
        public async Task<bool> Create(Post entity ) {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // Удаление поста
        public async Task<bool> Delete(Post entity) {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
