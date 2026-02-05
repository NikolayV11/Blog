using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Abstractions.Service.LikesPosts;
using System.Security.Claims;
using Blog.Core.Abstractions.Service.LikesComments;

namespace Blog.API.Controllers {
[ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikeController : ControllerBase {
        private readonly IPostLikeService _postLikeService;
        private readonly ILikeServices _commentLikeServices;

        public LikeController ( IPostLikeService postLikeService, ILikeServices commentLikeServices ) {
            _postLikeService = postLikeService;
            _commentLikeServices = commentLikeServices;
        }

        [HttpPost("comment/{commentId}")]
        public async Task<IActionResult> ToggleCommentLike(int commentId ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var result = await _commentLikeServices.ToggleAsync(userId, commentId);
            return Ok(new {isLiked =  result});
        }

        [HttpPost("post/{postId}")]
        public async Task<IActionResult> TogglePostLike ( int postId ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            // метод Tooggle сам решит : поставить или убрать лайк
            var result = await _postLikeService.TogglePostLikeAsync(userId, postId);

            return Ok(new {
                isLiked = result
            });
        }
    }
}
