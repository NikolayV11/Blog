using Blog.Contracts.DTO.Follow;

namespace Blog.Core.Abstractions.Service.Subscriptions {
    // Запросы: списки людей
    public interface ISubscriptionQueryService {
        Task<List<UserFollowerResponse>> GetFollowingAsync ( int userId );
        Task<List<UserFollowerResponse>> GetFollowersAsync ( int userId );
        Task<bool> IsFollowingAsync ( int followerId, int followingId );        
    }
}
