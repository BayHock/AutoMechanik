using System.ComponentModel.DataAnnotations;

namespace AutoMechanikCore.Models.Dtos.Account
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		public string Role { get; set; }
	}
}
