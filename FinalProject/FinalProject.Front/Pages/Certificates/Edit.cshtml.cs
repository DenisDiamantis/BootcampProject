using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
    public class EditModel : PageModel
    {
        private readonly CertificateService _context;

        public EditModel(CertificateService context)
        {
            _context = context;
        }

        [BindProperty]
        public CertificateUpdateDto Certificate { get; set; } = default;

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
            CertificateUpdateDto certificateUpdate = new CertificateUpdateDto();
            certificateUpdate.Cost = certificate.Cost;
            certificateUpdate.Description = certificate.Description;
            certificateUpdate.Title = certificate.Title;
            Certificate = certificateUpdate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _context.UpdateCertificateAsync(Certificate, id);
            return RedirectToPage("/Certificates/Index");
        }
    }
}
