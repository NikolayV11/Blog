

namespace Blog.Contracts.DTO.User {
    public record UserResponse (
        int Id,
        string FirstName,
        string LastName,
        string? Surname,
        string Email,
        string Phone,
        string? AvatarUrl
        );
}
