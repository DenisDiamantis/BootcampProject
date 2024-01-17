
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Helpers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FinalProject.Front.Services
{

	public class ExamService
	{
		private readonly HttpClient _httpClient;

		private readonly string _apiBaseUrl = "https://localhost:7193/api/Exam"; // Adjust the URL as per your backend API

		public ExamService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", ContextHelper.Token);
		}

		//Get all exam Templates
		public async Task<IEnumerable<ExamTemplateDto>> GetAllExamTemplates()
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<ExamTemplateDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		public async Task<IEnumerable<QuestionTemplateDto>> GetQuestionsTemplates(int examTemplateId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/GetQuestions/{examTemplateId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<QuestionTemplateDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Add questions
		public async Task<QuestionTemplateDto> AddQuestionAsync(QuestionTemplateDto question, int examTemplateId)
		{
			question.ExamTemplateId = examTemplateId;
			var content = new StringContent(JsonSerializer.Serialize(question), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}/AddQuestions", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<QuestionTemplateDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Create Template
		public async Task<ExamTemplateDto> CreateExamTemplateAsync(ExamTemplateDto examTemplateDto, int certId)
		{
			examTemplateDto.CertificateId = certId;
			var content = new StringContent(JsonSerializer.Serialize(examTemplateDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<ExamTemplateDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		//get Candidate Exams
		public async Task<IEnumerable<ExamDto>> GetAvailableExams(int userId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{userId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<ExamDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		//create Exam
		public async Task<ExamDto> CreateExamAsync(CertificateViewDto cert, int userId)
		{


			var response = await _httpClient.PostAsync($"{_apiBaseUrl}/createExam/{userId}/{cert.Id}", null);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<ExamDto>(responseContent,
															 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		//Get questions for exam
		public async Task<IEnumerable<QuestionTemplateDto>> GetQuestions(int examId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Exam/{examId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<QuestionTemplateDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		//Get results for exam
		public async Task<ExamDto> GetExamResults(int examId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/ExamResults/{examId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<ExamDto>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		//get all exams for admin
		public async Task<IEnumerable<ExamDto>> GetAllExams()
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Exams");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<ExamDto>>(responseContent,
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

		public async Task<ExamDto> UpdateExam(ExamDto examDto)
		{
			var content = new StringContent(JsonSerializer.Serialize(examDto), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync($"{_apiBaseUrl}/AddMarker", content);
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<ExamDto>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		public async Task<IEnumerable<ExamDto>> GetMarkerExams(int markerId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Marker/{markerId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<ExamDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		public async Task<ExamDto> GetExam(int examId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Evaluation/{examId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<ExamDto>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}

		public async Task<IEnumerable<AnswerDto>> getAnswers(int examId)
		{
			var response = await _httpClient.GetAsync($"{_apiBaseUrl}/Answers/{examId}");
			response.EnsureSuccessStatusCode();
			using var responseContent = await response.Content.ReadAsStreamAsync();
			var result = await JsonSerializer.DeserializeAsync<IEnumerable<AnswerDto>>(responseContent,
							   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
	}
}
