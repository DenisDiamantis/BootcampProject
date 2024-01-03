using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly CertificateService _context;

        public IndexModel(CertificateService certificateService)
        {
            _context = certificateService;
        }

        public IEnumerable<CertificateViewDto> Certificates { get; set; }

        public async Task OnGetAsync()
        {
            Certificates = await _context.GetAllCertificatesAsync();
        }

        //get certificate by id
        public async Task<IActionResult> OnGetCertificateByIdAsync(int id)
        {
            var certificate = await _context.GetCertificateByIdAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }
            return new JsonResult(certificate);
        }
    }
}
