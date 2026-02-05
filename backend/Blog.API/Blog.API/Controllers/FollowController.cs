using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Abstractions.Service.Subscriptions;

namespace Blog.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FollowController : Controller{

        private readonly ISubscriptionService _subService;
        private readonly ISubscriptionQueryService _sebQuery;

        public FollowController ( 
            ISubscriptionQueryService sebQuery, 
            ISubscriptionService subService ) {
            _subService = subService;
            _sebQuery = sebQuery;
        }

        // Подписаться / Отписаться
        [HttpPost("{followingId}")] // на кого пописан
        public async Task<IActionResult> ToggleFollow(int followingId ) {
            var followerId = int.Parse(User.FindFirst("userId")!.Value);

            if (followingId == followingId)
                return BadRequest("Нельзя подписатся на самого себя.");

            var result = await _subService.ToggleFollowService(followerId, followingId);
            return Ok(new {isFollowing = result});
        }

        // Получить список подписок
        [HttpGet("my-following")] // кто подписан
        public async Task<IActionResult> getMyFollowing ( ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var following = await _sebQuery.GetFollowingAsync(userId);
            return Ok(following);
        }
    }
}
