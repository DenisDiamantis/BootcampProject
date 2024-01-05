using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
    public class BuyModel : PageModel
    {
        private readonly CertificateService _context;

        public BuyModel(CertificateService context)
        {
            _context = context;
        }

        [BindProperty]
        public UserCertificateViewDto UserCertificate { get; set; } = default!;

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _context.CreateUserCertificateAsync(UserCertificate);

			return RedirectToPage("/Certificates/Index");
		}
		
    }
}
