using AutoMechanikCore.Models;
using System.Net.Http.Json;

namespace AutoMechanikCore.Services
{
	public class ApiApplicationService
	{
		private readonly HttpClient _httpClient;

		public ApiApplicationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<ApplicationModel>> GetApplicationsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<ApplicationModel>>("api/applications");
		}

		public async Task CreateApplicationAsync(ApplicationModel application)
		{
			await _httpClient.PostAsJsonAsync("api/applications", application);
		}
	}
}
