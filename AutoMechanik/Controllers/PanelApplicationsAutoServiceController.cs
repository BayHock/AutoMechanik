using AutoMechanik.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoMechanik.Controllers
{
	[Authorize(Roles = "AutoService")]
	public class PanelApplicationsAutoServiceController : Controller
	{
		private readonly AutoMechanikDbContext _context;

		public PanelApplicationsAutoServiceController(AutoMechanikDbContext context)
		{
			_context = context;
		}
		public IActionResult PanelApplicationsAutoServicePage()
		{		
			return View();
		}
	}
}
