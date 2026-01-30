using Blog.DataAccess.Models.Common;

namespace Blog.DataAccess.Models.User.Entity {
    public class User : BaseEntity {
        public string FirstName { get; set; }   // имя
        public string LastName { get; set; }    // фамилия
        public string Surname { get; set; } // Отчество
        public DateTime Birthday { get; set; }  // датарождения
        public string PasswordHash { get; set; }    // хеш пароля
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? AvatarId {  get; set; }
        public UserImages? Avatar {  get; set; }
    }
}
