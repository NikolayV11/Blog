// То что отдаем клиенту
using Blog.Contracts.DTO.Comment;

namespace Blog.Contracts.DTO.Post {
    public record PostResponse (
        int Id,
        string? Content,
        DateTime CreatedAt,
        int AuthorId,
        string AuthorFullName,
        string? AuthorAvatarUrl,
        int LikesCount,
        int CommentsCount,
        List<CommentResponse> Comments,
        List<string> ImageUrl
        );
}
