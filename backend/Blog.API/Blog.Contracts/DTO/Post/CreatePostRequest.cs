namespace Blog.Contracts.DTO.Post {
    // Получение от пользователя, пока без изображений
    public record CreatePostRequest (
        string? Content
        );
}
