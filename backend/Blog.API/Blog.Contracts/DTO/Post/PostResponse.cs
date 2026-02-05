// То что отдаем клиенту
namespace Blog.Contracts.DTO.Post {
    public record PostResponse (
        int Id,
        string? Content,
        DateTime CreatedAt,
        int AuthorId,
        string AuthorFullName,
        int LikesCount,
        int CommentsCount,
        List<string> ImageUrl
        );
}
