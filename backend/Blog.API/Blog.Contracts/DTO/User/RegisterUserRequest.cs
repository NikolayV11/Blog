namespace Blog.Contracts.DTO.User {
    // Регистрация
    public record RegisterUserRequest (
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string? Surname,
        string Phone,
        DateTime Birthday
        );
}
