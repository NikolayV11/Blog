namespace Blog.Core.Abstractions.Service.Subscriptions {
    // Команда: подписаться/отписаться
    public interface ISubscriptionService {
        Task<bool> ToggleFollowService ( int currentUserId, int targetUserId );
    }
}
