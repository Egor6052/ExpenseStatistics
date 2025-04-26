using Microsoft.AspNetCore.Mvc;
using ExpenseStatistics.Dto;
using ExpenseStatistics.Services;

namespace ExpenseStatistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto.Email, dto.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            return Ok(new { Token = token });
        }

    }
}
