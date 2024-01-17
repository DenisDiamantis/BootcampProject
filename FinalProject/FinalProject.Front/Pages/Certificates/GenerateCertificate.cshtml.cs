using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FinalProject.Front.Pages.Certificates
{
    public class GenerateCertificateModel : PageModel
    {

        private readonly CertificateService _certificateHelper;
        private readonly IRazorViewEngine _razorViewEngine;


        public GenerateCertificateModel(CertificateService certificateService, IRazorViewEngine razorViewEngine)
        {

            _certificateHelper = certificateService;
            _razorViewEngine = razorViewEngine;

        }


        public CertificateViewDto Certificate { get; set; }


        public async Task OnGetAsync(int certId)
        {

            Certificate = await _certificateHelper.GetCertificateByIdAsync(certId);


        }
    }
}
