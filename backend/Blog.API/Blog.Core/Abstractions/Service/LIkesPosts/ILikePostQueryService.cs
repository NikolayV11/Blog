namespace Blog.Core.Abstractions.Service.LikesPosts {
    public interface ILikePostQueryService {
        // узнать общее количество лайков поста
        Task<int> GetLikesCountAsync ( int postid );

        // проверка ставил ли пользователь лайк
        Task<bool> IsLikedByUserAsync ( int userId, int postId );
    }
}
