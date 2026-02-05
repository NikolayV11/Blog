using Blog.Contracts.DTO.User;
using Blog.Core.Abstractions.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase {
        private readonly IIdentityService _identityService;
        private readonly IUserQueryService _userQuery;

        public IdentityController ( IIdentityService identityService, IUserQueryService userQueryService ) {
            _identityService = identityService;
            _userQuery = userQueryService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ( [FromBody] RegisterUserRequest request ) {
            // Магия: запрос пройдёт через API -> Service -> Repository -> MySQL
            var result = await _identityService.RegisterAsync( request );

            if (result)
                return Ok("Пользователь успешно зарегистрирован!");

            return BadRequest("Ошибка регистрации. Возможно, Email уже занят");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe ( ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);
            var response = await _userQuery.GetUserResponseByIdAsync(userId);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile ( [FromForm] UpdateUserRequest request ) {
            var userId = int.Parse(User.FindFirst("userId")!.Value);

            var result = await _identityService.UpdateProfileAsync(userId, request);

            if (!result)
                return BadRequest("Не удалось обновить профи Murphy");

            return Ok(new { message = "Профиль и аватар обновлены" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login ( [FromBody] LoginUserRequest request ) {
            try {
                var token = await _identityService.LoginAsync(request);
                return Ok(new { Token = token });
            } catch (Exception ex) {
                // Если пароль неверный или пользователя нет
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
