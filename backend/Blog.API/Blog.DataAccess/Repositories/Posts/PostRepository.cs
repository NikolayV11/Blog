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
                    // Аватар автора поста
                    .ThenInclude(a => a.Avatar) 
                .Include(p => p.Images)
                // Загружаем сами комментарии
                .Include(p => p.Commentes)  
                    // Для каждого комментария грузим автора
                    .ThenInclude(c => c.Author) 
                        // Для автора комментария грузим аватар
                        .ThenInclude(ca => ca.Avatar)   
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
