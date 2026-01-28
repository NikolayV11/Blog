
namespace Blog.Core.Models.User.Entity {
    public class User : BaseEntity {
        public string FirstName { get; set; }   // имя
        public string LastName { get; set; }    // фамилия
        public DateTime Birthday { get; set; }  // датарождения
        public string PasswordHash { get; set; }    // хеш пароля
        public string Email { get; set; }
        public string Phone {  get; set; }
    }
}
