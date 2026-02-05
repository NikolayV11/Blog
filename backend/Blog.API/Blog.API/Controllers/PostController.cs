using Blog.Contracts.DTO.Post;
using Blog.Core.Abstractions.Service.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // чтобы проверялся токен
    public class PostController : ControllerBase {
        private readonly IPostService _postService;
        private readonly IPostQueryService _postQueryService;

        // Контроллер: ID сам подставит сюда нужные реализации
        public PostController ( IPostService postService, IPostQueryService postQueryService ) {
            _postService = postService;
            _postQueryService = postQueryService;
        }

        // 1. Конечная точка: Получает все посты
        [HttpGet]
        public async Task<ActionResult<List<PostResponse>>> GetPost ( ) {
            var posts = await _postQueryService.GetPostsAsync();
            return Ok(posts); // Возвращает статус 200 и список постов.
        }

        // 2. Конечная точка: создаст пост
        [HttpPost]
        public async Task<ActionResult<int>> CreatePost ( [FromForm] CreatePostRequest request ) {
            // 1. Достаним ID пользователя из Claims токена
            // "userId" - это ключь который указали в JwsProvider
            var userIdString = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId)) {
                return Unauthorized("Не удалось определить пользователя из токена");
            }

            var postId = await _postService.CreatePostAsync(userId, request);

            if (postId == 0)
                return BadRequest("Не удалост создать пост.");

            return Ok(postId);
        }

        // 3. Конечная точка: Получение поста по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> GetPost(int id ) {
            var post = await _postQueryService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound("Пост не найден.");
            return Ok(post);
        }
    }
}
