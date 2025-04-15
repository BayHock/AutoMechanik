using AutoMechanikCore.Models.Dtos.Account;
using AutoMechanikCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoMechanikMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApiAuthService _authService;

		public AccountController(ApiAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var result = await _authService.Register(dto);
			return result ? RedirectToAction("Index", "HomeUser") : View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			var result = await _authService.Login(dto);
			return result ? RedirectToAction("Index", "Home") : View("Error");
		}
	}
}
