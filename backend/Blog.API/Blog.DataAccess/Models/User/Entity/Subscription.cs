using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.User.Entity {
    public class Subscription : BaseEntity {
        // Кто подписывается
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        // На кого подписываются
        public int FollowingId { get; set; }
        public User Following { get; set; }
    }
}
