using AutoMechanik.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMechanik.Areas.Identity.Data;

public class AutoMechanikDbContext : IdentityDbContext<AutoMechanikUser, AutoMechanikRole, string>
{
	public AutoMechanikDbContext()
	{
	}

	public AutoMechanikDbContext(DbContextOptions<AutoMechanikDbContext> options)
		: base(options)
	{
	}

	public DbSet<ApplicationModel> Applications { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<ApplicationModel>()
			.Property(x => x.Id);
	}
}
