using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class DetailsModel : PageModel
	{
		private readonly ExamService _context;

		public DetailsModel(ExamService context)
		{
			_context = context;
		}

		public ExamTemplateDto Certificate { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			return Page();
		}
	}
}
