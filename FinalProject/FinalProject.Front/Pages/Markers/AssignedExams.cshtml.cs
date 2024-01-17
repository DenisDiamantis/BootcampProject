using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinalProject.Front.Pages.Markers
{
	public class AssignedExamsModel : PageModel
	{

		private readonly ExamService _context;
		private readonly CertificateService _certificateHelper;


		public AssignedExamsModel(ExamService examService, CertificateService certificateService)
		{
			_context = examService;
			_certificateHelper = certificateService;
		}

		public IEnumerable<CertificateViewDto> Certificates { get; set; }
		public IEnumerable<ExamDto> Exams { get; set; }


		public async Task OnGetAsync()
		{
			Certificates = await _certificateHelper.GetAllCertificatesAsync();
			Exams = await _context.GetMarkerExams(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));
		}
	}
}

