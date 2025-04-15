using AutoMechanikCore.Data;
using AutoMechanikCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMechanikApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationsController : ControllerBase
	{
		private readonly AutoMechanikDbContext _context;

		public ApplicationsController(AutoMechanikDbContext context)
		{
			_context = context;
		}

		// GET: api/applications
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications()
		{
			return await _context.Applications.ToListAsync();
		}

		// POST: api/applications
		[HttpPost]
		public async Task<ActionResult<ApplicationModel>> CreateApplication(ApplicationModel application)
		{
			_context.Applications.Add(application);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetApplications), application);
		}
	}
}
