namespace AutoMechanik.Models
{
	public interface IApplication
	{

	}

	public class ApplicationModel : IApplication
	{
		public Guid Id { get; set; } // уникальный id заявки
		public string? Number { get; set; } // номер заявки
		public string? NameService { get; set; } // название услуги в заявке
		public string? Description { get; set; } // описание заявки
		public string? Status { get; set; } // статус заявки
		public DateTime? DateCreate { get; set; } // дата создания заявки
	}
}