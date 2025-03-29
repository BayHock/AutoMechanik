using AutoMechanikCore.Data;
using AutoMechanikCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AutoMechanik.Controllers
{
	[Authorize(Roles = "AutoService")]
	public class PanelApplicationsAutoServiceController : Controller
	{
		private readonly AutoMechanikDbContext _context;
		private readonly ILogger<PanelApplicationsAutoServiceController> _logger;

		public PanelApplicationsAutoServiceController (ILogger<PanelApplicationsAutoServiceController> logger, AutoMechanikDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<ActionResult> PanelApplicationsAutoServicePage()
		{
			return View(await _context.Applications.ToListAsync());
		}

		[HttpPost]
		public async Task<ActionResult> AddApplicationDb(ApplicationModel applicationModel)
		{
			_context.Applications.Add(applicationModel);
			await _context.SaveChangesAsync();
			return RedirectToAction("PanelApplicationsAutoServicePage");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
