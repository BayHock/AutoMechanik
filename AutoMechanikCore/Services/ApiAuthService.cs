using AutoMechanikCore.Models.Dtos.Account;
using System.Net.Http.Json;

namespace AutoMechanikCore.Services
{
	public class ApiAuthService
	{
		private readonly HttpClient _httpClient;

		public ApiAuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
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
