using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class ExamResultsModel : PageModel
	{
		private readonly ExamService _context;

		public ExamResultsModel(ExamService context, CertificateService certificateHelper)
		{
			_context = context;
		}


		public ExamDto Exam { get; set; }
		public async Task OnGetAsync(int examId)
		{
			Exam = await _context.GetExamResults(examId);
		}
	}
}
