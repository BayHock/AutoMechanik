using AutoMechanikCore.Models.Dtos.Account;

namespace AutoMechanikMVC.Services
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("ApiClient");
		}

		public async Task<bool> Register(RegisterDto registerDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Login(LoginDto loginDto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
			return response.IsSuccessStatusCode;
		}
	}
}
