using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.User.Entity {
    public class UserImages : BaseEntity {

        public string OriginalName { get; set; }
        public string StoredName { get; set; }
        public string ContentType { get; set; }

        // Внешний ключ на пользователя
        public int UserId { get; set; }
        public User? User { get; set; } // Навигационное свойство
    }
}