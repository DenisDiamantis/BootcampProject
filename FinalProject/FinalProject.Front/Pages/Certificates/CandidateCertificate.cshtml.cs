using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinalProject.Front.Pages.Certificates
{
    public class CandidateCertificateModel : PageModel
    {

        private readonly UserCertificateService _userCertificateService;



        public CandidateCertificateModel(UserCertificateService userCertificateService)
        {

            _userCertificateService = userCertificateService;

        }


        public IEnumerable<UserCertificateViewDto> UserCertificates { get; set; }


        public async Task OnGetAsync()
        {

            UserCertificates = await _userCertificateService.GetUserCertificatesById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));

        }
    }
}
