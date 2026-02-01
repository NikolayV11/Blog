namespace Blog.Contracts.DTO.Follow {
    // С кем хотим подружится
    public record FollowRequest (
        int TargetUserId
        );
}
