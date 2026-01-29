namespace Blog.Core.Models {
    public class User {
        public int MAX_LASTNAME_LENGTH, MAX_FIRSTNAME_LENGTH = 25;
        public int Id { get; }
        public string Lastname { get; } // фамилия
        public string Firstname { get; } // имя
        public string? Surname { get; } // отчество
        public DateTime Birthday { get; }
        public string Email { get;  }
        public string Phone { get; }

        private User (
            int id, string lastName,
            string firstName, string? surName,
            DateTime birthday, string email,
            string phone) {
            Id = id;
            Lastname = lastName;
            Firstname = firstName;
            Surname = surName;
            Birthday = birthday;
            Email = email;
        }

        public static (User user, string error) Create ( int id, string lastName,
            string firstName, string? surName,
            DateTime birthday, string email,
            string phone) {
            User user = new User(id, lastName,
                            firstName, surName,
                            birthday,  email,
                            phone);
            return (user,"");

        }
    }
}
