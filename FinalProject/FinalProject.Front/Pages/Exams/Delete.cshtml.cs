using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class DeleteModel : PageModel
	{
		private readonly ExamService _context;

		public DeleteModel(ExamService context)
		{
			_context = context;
		}

		[BindProperty]
		public CertificateViewDto Certificate { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			//var certificate = await _context.GetCertificateByIdAsync(id.Value);


			return RedirectToPage("./Index");
		}
	}
}
