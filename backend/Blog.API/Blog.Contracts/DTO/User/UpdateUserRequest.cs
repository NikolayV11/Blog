using Microsoft.AspNetCore.Http;

namespace Blog.Contracts.DTO.User {
    public record UpdateUserRequest (
        string FirstName,
        string LastName,
        string? Surname,
        string Phone,
        DateTime Birthday,
        IFormFile? AvatarFile
        );
}
