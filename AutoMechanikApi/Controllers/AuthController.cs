using AutoMechanikApi.Services;
using AutoMechanikCore.Models.Dtos.Account;
using Microsoft.AspNetCore.Mvc;

namespace AutoMechanikApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			var result = await _authService.Register(dto);
			return result.Success
				? Ok(result)
				: BadRequest(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var result = await _authService.Login(dto);
			return result.Success
				? Ok(result)
				: BadRequest(result);
		}
	}
}
