using Blog.Core.Abstractions.Service.Posts;
using Blog.Core.Abstractions.Repository;
using Blog.Contracts.DTO.Post;
using Blog.DataAccess.Models.Post.Entity;

namespace Blog.DataAccess.Repositories.Posts {
    public class PostQueryService :
        IPostQueryService
        {
        private readonly IGetRepository<Post> _postRepo;

        public PostQueryService(IGetRepository<Post> postRepo) {
            _postRepo = postRepo;
        }

        // получаем посты
        public async Task<List<PostResponse>> GetPostsAsync ( ) {
            var posts = await _postRepo.Get();

            var postList = posts.Select(
                p => new PostResponse(
                    p.Id,
                    p.Content,
                    p.CreatedAt,
                    p.UserId,
                    $"{p.Author.FirstName} {p.Author.LastName}",
                    p.Likes.Count,
                    p.Commentes.Count
                    )).ToList();

            return postList;
        }

        // Получаем пост по ID
        public async Task<PostResponse?> GetPostByIdAsync(int postId ) {
            var posts = await _postRepo.Get();

            var post = posts.FirstOrDefault(p => p.Id == postId);

            if (post == null)
                return null;

            PostResponse response = new PostResponse(
                post.Id,
                post.Content,
                post.CreatedAt,
                post.UserId,
                $"{post.Author.FirstName} {post.Author.LastName}",
                post.Likes.Count,
                post.Commentes.Count
                );

            return response;
        }

        public async Task<List<PostResponse>> GetUserPostsAsync(int userId ) {
            var allPosts = await GetPostsAsync();
            var postsUser = allPosts.Where(p => p.Id ==  userId).ToList();

            return postsUser;
        }
    }
}
