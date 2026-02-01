using Blog.DataAccess.Models.Common;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.DataAccess.Models.User.Entity {
    public class User : BaseEntity {
        public string FirstName { get; set; } = string.Empty;   // имя
        public string LastName { get; set; } = string.Empty;   // фамилия
        public string? Surname { get; set; } // Отчество
        public DateTime Birthday { get; set; }  // датарождения
        public string PasswordHash { get; set; } = string.Empty;   // хеш пароля
        public string Email { get; set; } =string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? AvatarId { get; set; }
        public UserImages? Avatar { get; set; }
        public List<Post.Entity.Post> Posts { get; set; } = new();
        public List<Commentes> Commentes { get; set; } = new();
        public List<LikePost> LikePosts { get; set; } = new();
        public List<LikeComment> LikeComments { get; set; } = new();
        // Те, кто подписан на тебя (Твои подписчики)
        public List<Subscription> Followers { get; set; } = new();

        // Те, на кого подписан ТЫ (Твои подписки)
        public List<Subscription> Following { get; set; } = new();
    }
}
