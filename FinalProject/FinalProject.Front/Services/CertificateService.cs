
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FinalProject.Front.Services
{

	public class CertificateService
	{
		private readonly HttpClient _httpClient;
		private readonly IContextHelper _contextHelper;
		private readonly string _apiBaseUrl = "https://localhost:7193/api/certificate"; // Adjust the URL as per your backend API

		public CertificateService(HttpClient httpClient, IContextHelper contextHelper)
		{
			_httpClient = httpClient;
			_contextHelper = contextHelper;
			_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _contextHelper.Token);
		}

		//Get all certificate
		public async Task<IEnumerable<CertificateViewDto>> GetAllCertificatesAsync()
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<CertificateViewDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Get certificate by id
		public async Task<CertificateViewDto> GetCertificateByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<CertificateViewDto>(responseContent,
											  new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Create certificate
		public async Task<CertificateViewDto> CreateCertificateAsync(CertificateViewDto certificateViewDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(certificateViewDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<CertificateViewDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Update certificate
		public async Task<CertificateViewDto> UpdateCertificateAsync(CertificateViewDto certificateViewDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(certificateViewDto), Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{certificateViewDto.Id}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<CertificateViewDto>(responseContent,
					 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Delete certificate
		public async Task DeleteCertificateAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
			response.EnsureSuccessStatusCode();
		}

		public async Task<UserCertificateViewDto> CreateUserCertificateAsync(UserCertificateViewDto userCertificateViewDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(userCertificateViewDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<UserCertificateViewDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}


	}
}
