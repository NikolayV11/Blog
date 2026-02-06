using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Abstractions.Service.Comments;

namespace Blog.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentController : ControllerBase {
        private readonly ICommentService _commentService;

        public CommentController ( ICommentService commentService ) {
            _commentService = commentService;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> CreateComment ( int postId, [FromBody] string content ) {
            // извлекаем id из токена
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            if (string.IsNullOrWhiteSpace(content))
                return BadRequest("Комментарий не может быть пустым.");

            var commentId = await _commentService.CreateCommentAsync(userId, postId, content);

            if(commentId == 0) {
                return BadRequest("Не удалось оставить комментарий");
            }

            return Ok(new {CmmentId = commentId});
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var result = await _commentService.DeleteCommentAsync(userId, commentId);

            if (!result)
                return BadRequest("Ошибка удаления или у вас нет прав");

            return Ok(new { messege = "Комментарий удален" });
        }
    }
}
