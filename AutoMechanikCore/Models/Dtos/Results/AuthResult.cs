namespace AutoMechanikCore.Models.Dtos.Results
{
	public class AuthResult
	{
		public bool Success { get; set; }         // Флаг успеха операции
		public string? Error { get; set; }        // Одно сообщение об ошибке
		public IEnumerable<string>? Errors { get; set; }  // Список ошибок

		public static AuthResult Ok()
			=> new() { Success = true};

		public static AuthResult Fail(string error)
			=> new() { Success = false, Error = error };

		public static AuthResult Fail(IEnumerable<string> errors)
			=> new() { Success = false, Errors = errors };
	}
}
