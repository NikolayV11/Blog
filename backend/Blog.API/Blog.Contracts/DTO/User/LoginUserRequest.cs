namespace Blog.Contracts.DTO.User {
    // Вход
    public record LoginUserRequest (
        string Email,
        string Password
        );
    
}
