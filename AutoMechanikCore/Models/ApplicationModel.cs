using System.ComponentModel.DataAnnotations;

namespace AutoMechanikCore.Models
{
	public class ApplicationModel()
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
	}
}