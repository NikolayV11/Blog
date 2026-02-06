namespace Blog.Contracts.DTO.Comment {
    public record CommentResponse (
        int Id,
        string Content,
        DateTime CreatedAt,
        string AuthorName,
        string? AuthorAvatarUrl
        );
}
