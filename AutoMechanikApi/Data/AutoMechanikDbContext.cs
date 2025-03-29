using Microsoft.EntityFrameworkCore;

namespace AutoMechanikApi.Data
{
	public class AutoMechanikDbContext : DbContext
	{
		public AutoMechanikDbContext(DbContextOptions<AutoMechanikDbContext> options): base(options)
		{
		}
	}
}
