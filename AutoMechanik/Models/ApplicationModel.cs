using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMechanik.Models
{
	public class ApplicationModel()
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
	}
}