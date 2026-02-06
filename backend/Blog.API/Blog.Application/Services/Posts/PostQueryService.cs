using Blog.Core.Abstractions.Service.Posts;
using Blog.Core.Abstractions.Repository;
using Blog.Contracts.DTO.Post;
using Blog.DataAccess.Models.Post.Entity;
using Blog.DataAccess.Repositories.Comments;
using Blog.Contracts.DTO.Comment;

namespace Blog.Application.Services.Posts {
    public class PostQueryService :
        IPostQueryService {
        private readonly IGetRepository<Post> _postRepo;

        public PostQueryService ( IGetRepository<Post> postRepo ) {
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
                    p.Author.Avatar?.StoredName != null ? $"/uploads/{p.Author.Avatar.StoredName}" : null,
                    p.Likes.Count,
                    p.Commentes.Count,
                    p.Commentes.Select(c => new CommentResponse(
                        c.Id,
                        c.Content,
                        c.CreatedAt,
                        $"{c.Author.FirstName} {c.Author.LastName}",
                        c.Author.Avatar != null ? $"{c.Author.Avatar.StoredName}" : null
                        )).ToList(),
                    p.Images.Select(img => $"uploads/{img.StoredName}").ToList()
                    )).ToList();

            return postList;
        }

        // Получаем пост по ID
        public async Task<PostResponse?> GetPostByIdAsync ( int postId ) {
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
                post.Author.Avatar?.StoredName != null ? $"/uploads/{post.Author.Avatar.StoredName}" : null,
                post.Likes.Count,
                post.Commentes.Count,
                post.Commentes.Select(c => new CommentResponse(
                    c.Id,
                    c.Content,
                    c.CreatedAt,
                    $"{c.Author.FirstName} {c.Author.LastName}",
                    c.Author.Avatar != null ? $"{c.Author.Avatar.StoredName}" : null
                    )).ToList(),
                post.Images.Select(img => $"uploads/{img.StoredName}").ToList()
                );

            return response;
        }

        public async Task<List<PostResponse>> GetUserPostsAsync ( int userId ) {
            var allPosts = await GetPostsAsync();
            var postsUser = allPosts.Where(p => p.AuthorId == userId).ToList();

            return postsUser;
        }
    }
}
