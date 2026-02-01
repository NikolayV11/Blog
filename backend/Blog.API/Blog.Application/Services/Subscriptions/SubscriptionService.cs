using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service.Subscriptions;
using Blog.DataAccess.Models.User.Entity;

namespace Blog.Application.Services.Subscriptions {
    public class SubscriptionService : ISubscriptionService {
        private readonly ISubscriptionQueryService _queryService;
        private readonly ICreateRepository<Subscription> _createRepo;
        private readonly IDeleteRepository<Subscription> _deleteRepo;
        // Чтобы найти существующую запись для удаления
        private readonly IGetRepository<Subscription> _getRepo;

        public SubscriptionService ( 
            ISubscriptionQueryService queryService, 
            ICreateRepository<Subscription> createRepo, 
            IDeleteRepository<Subscription> deleteRepo, 
            IGetRepository<Subscription> getRepo ) {
            _queryService = queryService;
            _createRepo = createRepo;
            _deleteRepo = deleteRepo;
            _getRepo = getRepo;
        }

        public async Task<bool> ToggleFollowService ( int currentUserId, int targetUserId ) {
            // 1. Бизнес-правило: нельзя подписыватся на себя
            if (currentUserId == targetUserId) {
                throw new Exception("Вы не можете подписатся на себя.");
            }

            // 2. Проверяем, существует ди уже такая подписка
            var isFollowing = await _queryService
                .IsFollowingAsync(currentUserId, targetUserId);

            if (isFollowing) {
                // 3. Если подписан - отписываемся
                // Ищем конкретную запись в базе, чтобы е удалить
                var allSubs = await _getRepo.Get();
                var sub = allSubs.FirstOrDefault(
                    s => s.FollowerId == currentUserId &&
                    s.FollowingId == targetUserId);

                if (sub != null) {
                    return await _deleteRepo.Delete(sub);
                }
                return false;
            }
            else {
                // 4. Если не подписан - создаем новую связ
                var newSub = new Subscription {
                    FollowerId = currentUserId,
                    FollowingId = targetUserId,
                    CreatedAt = DateTime.UtcNow
                };

                return await _createRepo.Create(newSub);
            }
        }
    }
}
