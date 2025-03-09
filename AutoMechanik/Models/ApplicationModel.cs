using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMechanik.Models
{
	public class ApplicationModel()
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } // уникальный id заявки
		public string? Number { get; set; } // номер заявки
		public string? NameService { get; set; } // название услуги в заявке
		public string? Description { get; set; } // описание заявки
		public string? Status { get; set; } // статус заявки
		public DateTime? DateCreate { get; set; } // дата создания заявки
	}
}