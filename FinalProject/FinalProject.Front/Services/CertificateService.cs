
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Entities;
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
		public async Task<CertificateCreateDto> CreateCertificateAsync(CertificateCreateDto certificate)
		{
            using var formData = new MultipartFormDataContent();

            // Add other properties
            formData.Add(new StringContent(certificate.Title), "Title");
            formData.Add(new StringContent(certificate.Description), "Description");
            formData.Add(new StringContent(certificate.Cost.ToString()), "Cost");

            // Add the file
            if (certificate.UploadedImage != null)
            {
                var fileContent = new StreamContent(certificate.UploadedImage.OpenReadStream());
                formData.Add(fileContent, "UploadedImage", certificate.UploadedImage.FileName);
            }


            var response = await _httpClient.PostAsync($"{_apiBaseUrl}", formData);
            var debug12 = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<CertificateCreateDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Update certificate
		public async Task<CertificateViewDto> UpdateCertificateAsync(CertificateUpdateDto certificateUpdateDto, int id)
		{
			var content = new StringContent(JsonSerializer.Serialize(certificateUpdateDto), Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);
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

        public async Task<byte[]> GetCertificateImageUrlAsync(string imageName)
        {
            // Make a call to your backend API to get the image URL
            var response = await _httpClient.GetAsync($"https://localhost:7193/image/{imageName}");

            if (response.IsSuccessStatusCode)
            {
                // Assuming the response contains the image bytes as a byte array
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                // Log or inspect the response content for details
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, {responseContent}");

                // Return null or throw an exception based on your error handling strategy
                return null;
            }
        }
    }
}
