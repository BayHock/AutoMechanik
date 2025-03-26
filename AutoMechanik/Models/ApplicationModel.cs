using System.ComponentModel.DataAnnotations;

namespace AutoMechanik.Models
{
	public class ApplicationModel()
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
	}
}