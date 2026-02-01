namespace Blog.Core.Abstractions.Service.LikesComments {
    public interface ILikeServices {
        Task<bool> ToggleAsync ( int userId, int commetId );
    }
}
