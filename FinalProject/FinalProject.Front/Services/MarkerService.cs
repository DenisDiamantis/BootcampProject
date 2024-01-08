using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using System.Text;
using System.Text.Json;

namespace FinalProject.Front.Services
{

	public class MarkerService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiBaseUrl = "https://localhost:7193/api/marker"; // Adjust the URL as per your backend API

		public MarkerService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		//Get all markers
		public async Task<IEnumerable<MarkerDto>> GetAllMarkersAsync()
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<MarkerDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Get marker by id
		public async Task<MarkerDto> GetMarkerByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<MarkerDto>(responseContent,
											  new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Get marker by user id
		public async Task<MarkerDto> GetMarkerByUserIdAsync(int userId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/user/{userId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<MarkerDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		//++

		//Create marker
		public async Task<MarkerDto> CreateMarkerAsync(MarkerDto markerDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(markerDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<MarkerDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Update marker
		public async Task<MarkerDto> UpdateMarkerAsync(MarkerDto markerDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(markerDto), Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{markerDto.Id}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<MarkerDto>(responseContent,
					 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Delete marker
		public async Task DeleteMarkerAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
			response.EnsureSuccessStatusCode();
		}


	}
}
