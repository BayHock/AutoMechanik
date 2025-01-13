using AutoMechanik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMechanik.Controllers
{
	[Authorize]
	public class HomeUserController : Controller
	{
		private readonly ILogger<HomeUserController> _logger;

		public HomeUserController(ILogger<HomeUserController> logger)
		{
			_logger = logger;
		}
		public IActionResult HomeUserPage()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
