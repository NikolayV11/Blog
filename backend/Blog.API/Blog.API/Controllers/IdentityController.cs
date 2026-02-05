using Blog.Contracts.DTO.User;
using Blog.Core.Abstractions.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase {
        private readonly IIdentityService _identityService;

        public IdentityController ( IIdentityService identityService ) {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register ( [FromBody] RegisterUserRequest request ) {
            // Магия: запрос пройдёт через API -> Service -> Repository -> MySQL
            var result = await _identityService.RegisterAsync( request );

            if (result)
                return Ok("Пользователь успешно зарегистрирован!");

            return BadRequest("Ошибка регистрации. Возможно, Email уже занят");
        }
    }
}
