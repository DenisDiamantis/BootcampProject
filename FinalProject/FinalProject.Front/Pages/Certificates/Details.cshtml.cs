using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Intrinsics.X86;

namespace FinalProject.Front.Pages.Certificates
{
	public class DetailsModel : PageModel
	{
		private readonly CertificateService _context;

		public DetailsModel(CertificateService context)
		{
			_context = context;
		}


        public byte[] ImageUrl { get; set; } = null;

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
                // Assuming ImageUrl contains the full URL, parse it to get the image name
                var imageName = Path.GetFileName(Certificate.ImageUrl);
                ImageUrl = await GetCertificateImageUrlAsync(imageName);

            }
            return Page();
        }

        public async Task<byte[]> GetCertificateImageUrlAsync(string imageName)
        {
            // Call your API endpoint to get the image URL based on the certificate ID
            return await _context.GetCertificateImageUrlAsync(imageName);
        }
    }
}
