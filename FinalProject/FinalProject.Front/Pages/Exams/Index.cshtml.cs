using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{

	public class IndexModel : PageModel
	{
		private readonly ExamService _context;
		private readonly CertificateService _certificateHelper;
		private readonly UserCertificateService _userCertificateService;
		private readonly CandidateService _candidateService;


		public IndexModel(CandidateService candidateService, ExamService examService, CertificateService certificateService, UserCertificateService userCertificateService)
		{
			_context = examService;
			_certificateHelper = certificateService;
			_userCertificateService = userCertificateService;
			_candidateService = candidateService;
		}

		public IEnumerable<ExamTemplateDto> ExamTemplates { get; set; }
		public IEnumerable<CertificateViewDto> Certificates { get; set; }
		public IEnumerable<ExamDto> Exams { get; set; }
		public IEnumerable<UserCertificateViewDto> UserCertificates { get; set; }
		public IEnumerable<CandidateDto> Candidates { get; set; }

		public async Task OnGetAsync()
		{
			ExamTemplates = await _context.GetAllExamTemplates();
			Certificates = await _certificateHelper.GetAllCertificatesAsync();
			Exams = await _context.GetAllExams();
			UserCertificates = await _userCertificateService.GetUserCertificates();
			Candidates = await _candidateService.GetAllCandidatesAsync();

		}
	}
}