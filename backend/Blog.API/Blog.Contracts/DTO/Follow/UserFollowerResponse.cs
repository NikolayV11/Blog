namespace Blog.Contracts.DTO.Follow {
    // кого увидем в списках (подписчики/подписки)
    public record UserFollowerResponse (
        int Id,
        string FullName,
        string? AvatarUrl
        );
}
