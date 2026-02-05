namespace Blog.Core.Abstractions.Auth {
    public interface IJwtProvaider {
        string GenerateToken ( Models.User user );
    }
}
