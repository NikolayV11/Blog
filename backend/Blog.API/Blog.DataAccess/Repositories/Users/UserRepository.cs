
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service;
using Blog.DataAccess.Models.User.Entity;
using Blog.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Users {
    // Наследуем Query (чтение), а остальные интерфейсы реализуем прямо здесь
    public class UserRepository 
        : BaseQueryRepository<User>
        , ICreateRepository<User>
        ,IGetByEmailRepository<User>
        , IUpDateService<User> {
        public UserRepository(BlogDbContext context)
            : base(context) { }

        // Создание сущности
        public async Task<bool> Create(User user ) {
            await _dbSet.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        // поиск по Email
        public async Task<User?> GetByEmailAsync(string email ) {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
