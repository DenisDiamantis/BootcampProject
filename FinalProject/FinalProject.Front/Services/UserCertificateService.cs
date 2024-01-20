using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Helpers;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FinalProject.Front.Services
{

	public class UserCertificateService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiBaseUrl = "https://localhost:7193/api/userCertificate";

		public UserCertificateService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", ContextHelper.Token);
		}

		public async Task CreateUserCertificateAsync(int examId)
		{

			var response = await _httpClient.PostAsync($"{_apiBaseUrl}/{examId}", null);
			response.EnsureSuccessStatusCode();

		}

		public async Task<IEnumerable<UserCertificateViewDto>> GetUserCertificates()
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<UserCertificateViewDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		public async Task<IEnumerable<UserCertificateViewDto>> GetUserCertificatesById(int userId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{userId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<UserCertificateViewDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
	}
}
