using Blog.Core.Abstractions.Auth;

namespace Blog.Application.Auth {
    public class PasswordHasher : IPasswordHasher {
        // Хешируем пароль перед сохранением в БД
        public string Generate ( string password ) {
            return BCrypt.Net.BCrypt.EnhancedHashPassword( password );
        }
        // Проверяем, совпадает ли введенный пароль с хешем из базы
        public bool VerifyPassword ( string password, string hashedPassword ) {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
