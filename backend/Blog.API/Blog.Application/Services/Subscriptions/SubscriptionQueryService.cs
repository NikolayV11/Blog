using Blog.Contracts.DTO.Follow;
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service.Subscriptions;
using Blog.DataAccess.Models.User.Entity;

namespace Blog.Application.Services.Subscriptions {
    public class SubscriptionQueryService : ISubscriptionQueryService{
        private readonly IGetRepository<Subscription> _subRepo;
        private readonly IGetRepository<User> _userRepo;

        public SubscriptionQueryService ( 
            IGetRepository<Subscription> subRepo, 
            IGetRepository<User> userRepo ) {
            _subRepo = subRepo;
            _userRepo = userRepo;
        }

        // проверка: подписан ли один пользователь на другого
        public async Task<bool> IsFollowingAsync ( int followerId, int followingId ) {
            var allSub = await _subRepo.Get();
            return allSub.Any(
                s => s.FollowerId == followerId
                && s.FollowingId == followingId);
        }

        // Список тех на кого подписан я (Following)
        public async Task<List<UserFollowerResponse>> GetFollowingAsync ( int userId ) {
            var allSub = await _subRepo.Get();

            return allSub
                .Where(
                s => s.FollowerId == userId
                && s.Following != null)
                .Select(s => new UserFollowerResponse(
                    s.Following.Id,
                    $"{s.Following.FirstName} {s.Following.LastName}",
                    // Навигационное свойство из Entity
                    s.Following.Avatar?.StoredName)).ToList();
        }

        // Список тех кто подписан на меня(Followers)
        public async Task<List<UserFollowerResponse>> GetFollowersAsync ( int userId ) {
            var allSubs = await _subRepo.Get();

            var followers = allSubs.Where(
                s => s.FollowingId == userId
                && s.Follower != null)
                .Select(s => new UserFollowerResponse(
                    s.Follower.Id,
                    $"{s.Follower.FirstName} {s.Follower.LastName}",
                    s.Follower.Avatar?.StoredName
                    )).ToList();

            return followers;
        }

    }
}
