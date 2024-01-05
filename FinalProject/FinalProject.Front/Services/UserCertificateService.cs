
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FinalProject.Front.Services
{

	public class UserCertificateService
	{
		private readonly HttpClient _httpClient;
		private readonly IContextHelper _contextHelper;
		private readonly string _apiBaseUrl = "https://localhost:7193/api/userCertificate";

		public UserCertificateService(HttpClient httpClient, IContextHelper contextHelper)
		{
			_httpClient = httpClient;
			_contextHelper = contextHelper;
			_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _contextHelper.Token);
		}

		public async Task<UserCertificateCreateDto> CreateUserCertificateAsync(UserCertificateCreateDto userCertificateCreateDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(userCertificateCreateDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
			response.EnsureSuccessStatusCode();
			return null;
		}


	}
}
