using AutoMechanik.Areas.Identity.Data;
using AutoMechanik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMechanik.Controllers
{
	[Authorize(Roles = "AutoService")]
	public class HomeAutoServiceController : Controller
	{
		private readonly ILogger<HomeAutoServiceController> _logger;
		private readonly AutoMechanikDbContext _context;

		public HomeAutoServiceController(ILogger<HomeAutoServiceController> logger, AutoMechanikDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult HomeAutoServicePage()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(ApplicationModel applicationModel)
		{
			_context.Applications.Add(applicationModel);
			await _context.SaveChangesAsync();
			return RedirectToAction("HomeAutoServicePage");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
