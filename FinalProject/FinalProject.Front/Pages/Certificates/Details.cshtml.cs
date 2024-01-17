using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
	public class DetailsModel : PageModel
	{
		private readonly CertificateService _context;

		public DetailsModel(CertificateService context)
		{
			_context = context;
		}

		public CertificateViewDto Certificate { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var certificate = await _context.GetCertificateByIdAsync(id.Value);
			if (certificate == null)
			{
				return NotFound();
			}
			else
			{
				Certificate = certificate;
			}
			return Page();
		}
	}
}
