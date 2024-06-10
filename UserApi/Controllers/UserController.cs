using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Payload.Requests;
using UserApi.Services.UserService;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            await userService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await userService.LoginAsync(request);
            return Ok(response);
        }

        [HttpGet("getByToken"), Authorize]
        public async Task<IActionResult> GetByToken()
        {
            var user = await userService.GetByToken();
            return Ok(user);
        }
    }
}