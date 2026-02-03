using Blog.Core.Abstractions.Repository;
using Blog.DataAccess.Models.User.Entity;
using Blog.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Subscriptions {
    public class SubscriptionRepository
        : BaseQueryRepository<Subscription>,
        ICreateRepository<Subscription>,
        IDeleteRepository<Subscription>
        {
        public SubscriptionRepository(BlogDbContext context) : base(context) { }

        // Реализуем создание 
        public async Task<bool> Create(Subscription entity ) {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // Реализуем удаление
        public async Task<bool> Delete(Subscription entity ) {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
