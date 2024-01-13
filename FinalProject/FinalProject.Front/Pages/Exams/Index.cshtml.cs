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


		public IndexModel(ExamService examService, CertificateService certificateService)
		{
			_context = examService;
			_certificateHelper = certificateService;
		}

		public IEnumerable<ExamTemplateDto> ExamTemplates { get; set; }
		public IEnumerable<CertificateViewDto> Certificates { get; set; }


		public async Task OnGetAsync()
		{
			ExamTemplates = await _context.GetAllExamTemplates();
			Certificates = await _certificateHelper.GetAllCertificatesAsync();
		}

	}
}
