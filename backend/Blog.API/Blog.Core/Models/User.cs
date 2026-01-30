namespace Blog.Core.Models {
    public class User {
        public int Id { get; }
        public string Lastname { get; } // фамилия
        public string Firstname { get; } // имя
        public string? Surname { get; } // отчество
        public DateTime Birthday { get; }
        public string Email { get;  }
        public string Phone { get; }
        public int? AvatarId { get; }
        public Image? Avatar { get; }

        private User (
            int id, string lastName,
            string firstName, string? surName,
            DateTime birthday, string email,
            string phone, int? avatarId, Image? avatar ) {
            Id = id;
            Lastname = lastName;
            Firstname = firstName;
            Surname = surName;
            Birthday = birthday;
            Email = email;
            AvatarId = avatarId;
            Avatar = avatar;
        }

        public static (User user, string error) Create ( int id, string lastName,
            string firstName, string? surName,
            DateTime birthday, string email,
            string phone, int avatarId, Image avatar) {
            User user = new User(id, lastName,
                            firstName, surName,
                            birthday,  email,
                            phone, avatarId, avatar);
            return (user,"");

        }
    }
}
