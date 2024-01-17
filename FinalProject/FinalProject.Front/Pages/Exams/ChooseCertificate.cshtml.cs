using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
	public class ChooseCertifcateModel : PageModel
	{
		private readonly CertificateService _certificateHelper;
		public ChooseCertifcateModel(CertificateService certificateHelper)
		{
			_certificateHelper = certificateHelper;
		}

		public IEnumerable<CertificateViewDto> Certificates { get; set; }
		public async Task OnGetAsync()
		{
			Certificates = await _certificateHelper.GetAllCertificatesAsync();
		}

	}
}
