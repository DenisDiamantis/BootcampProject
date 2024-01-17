using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class EditModel : PageModel
	{
		private readonly ExamService _context;

		public EditModel(ExamService context)
		{
			_context = context;
		}

		[BindProperty]
		public CertificateViewDto Certificate { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}


			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _context.UpdateCertificateAsync(Certificate);
			return RedirectToPage("/Certificates/Index");
		}
	}
}
