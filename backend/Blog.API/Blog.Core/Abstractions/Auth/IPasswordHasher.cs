namespace Blog.Core.Abstractions.Auth {
    public interface IPasswordHasher {
        string Generate(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
