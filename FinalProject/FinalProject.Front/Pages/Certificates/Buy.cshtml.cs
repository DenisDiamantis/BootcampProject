using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FinalProject.Front.Pages.Certificates
{
    [Authorize(Roles = "admin,qualityAssurance")]
    public class BuyModel : PageModel
    {
        private readonly UserCertificateService _context;
        private readonly ILogger<BuyModel> _logger;

        public BuyModel(UserCertificateService context, ILogger<BuyModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public UserCertificateCreateDto UserCertificate { get; set; } = new UserCertificateCreateDto();

        public IActionResult OnGet()
        {
            // Access the UserCertificate.Id from the query string
            if (int.TryParse(Request.Query["id"], out var id))
            {
                // Set the UserCertificate.Id
                UserCertificate.Id = id;
            }
            else
            {
                _logger.LogError("Failed to parse 'id' from the query string.");
                // Handle the case where 'id' is not a valid integer
                ModelState.AddModelError(string.Empty, "Invalid 'id' in the query string.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _context.CreateUserCertificateAsync(UserCertificate);
                return RedirectToPage("/Certificates/Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating user certificate");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the user certificate.");
                return Page();
            }
        }
    }
}