using FinalProject.Data.Dtos;
using System.Text;
using System.Text.Json;

namespace FinalProject.Front.Services
{

    public class CandidateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7193/api/candidate"; // Adjust the URL as per your backend API

        public CandidateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //Get all candidates
        public async Task<IEnumerable<CandidateDto>> GetAllCandidatesAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<IEnumerable<CandidateDto>>(responseContent,
                               new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        //Get candidate by id
        public async Task<CandidateDto> GetCandidateByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<CandidateDto>(responseContent,
                                              new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        //Create candidate
        public async Task<CandidateDto> CreateCandidateAsync(CandidateDto candidateDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(candidateDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<CandidateDto>(responseContent,
                                                             new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        //Update candidate
        public async Task<CandidateDto> UpdateCandidateAsync(CandidateDto candidateDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(candidateDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{candidateDto.Id}", content);
            response.EnsureSuccessStatusCode();
            using var responseContent = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<CandidateDto>(responseContent,
                     new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        //Delete candidate
        public async Task DeleteCandidateAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }


    }
}
