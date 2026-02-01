namespace Blog.Contracts.DTO.Like {
    public record LikeCommentRequest (
        int CommentId
        );

    public record LikeCommentResponse (
        int CommentId,
        int TotalLikes,
        bool IsLikedByMe
        );
}
