using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class CreateExamModel : PageModel
	{
		private readonly ExamService _context;
		private readonly CertificateService _certificateHelper;
		private static int _certificateId;

		public CreateExamModel(ExamService context, CertificateService certificateHelper)
		{
			_context = context;
			_certificateHelper = certificateHelper;
		}

		public IEnumerable<CertificateViewDto> Certificates { get; set; }
		public async Task OnGetAsync(int id)
		{
			_certificateId = id;
			Certificates = await _certificateHelper.GetAllCertificatesAsync();
		}


		[BindProperty]
		public ExamTemplateDto ExamTemplate { get; set; }

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var result = await _context.CreateExamTemplateAsync(ExamTemplate, _certificateId);

			return RedirectToPage("/Exams/AddQuestions", new { numberOfQuetions = ExamTemplate.MaxScore, examTemplateId = result.Id });
		}
	}
}
