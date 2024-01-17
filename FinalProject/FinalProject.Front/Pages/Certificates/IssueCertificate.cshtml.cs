using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
	public class IssueCertificateModel : PageModel
	{
		private readonly CandidateService _candidateService;

		private readonly ExamService _context;

		private readonly CertificateService _certificateService;
		private readonly UserCertificateService _userCertificateService;
		private static int _examId;


		public IssueCertificateModel(UserCertificateService userCertificateService, CandidateService candidateService, ExamService context, CertificateService certificateService)
		{
			_candidateService = candidateService;
			_context = context;
			_certificateService = certificateService;
			_userCertificateService = userCertificateService;
		}

		public CandidateDto Candidate { get; set; }
		public ExamDto Exam { get; set; }

		public CertificateViewDto Certificate { get; set; }
		public UserCertificateViewDto UserCertificate { get; set; }
		public async Task OnGetAsync(int examId)
		{
			Exam = await _context.GetExam(examId);
			Candidate = await _candidateService.GetCandidateByNumberAsync(Exam.CandidateNumber);
			Certificate = await _certificateService.GetCertificateByIdAsync(Exam.CertificateId);
			_examId = examId;
		}

		public async Task<IActionResult> OnPostAsync()
		{

			await _userCertificateService.CreateUserCertificateAsync(_examId);
			return RedirectToPage("/Exams/Index");
		}
	}
}

