using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Services;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Certificates
{
	public class CreateModel : PageModel
	{
		private readonly CertificateService _context;
		private readonly IFileStorageService _fileStorageService;

		[BindProperty]
		public IFormFile UploadedImage { get; set; }

		[BindProperty]
		public CertificateCreateDto CertificateCreateDto { get; set; }


		public CreateModel(CertificateService context, IFileStorageService fileStorageService)
		{
			_context = context;
			_fileStorageService = fileStorageService;
		}

		public string ErrorMessage { get; private set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			try
			{
				// Save the uploaded file and get the file path
				string imagePath = _fileStorageService.SaveFile(UploadedImage);

				// Create a Certificate entity using the CertificateCreateDto and file path
				CertificateCreateDto certificate = new CertificateCreateDto
				{
					Title = CertificateCreateDto.Title,
					Description = CertificateCreateDto.Description,
					Cost = CertificateCreateDto.Cost,
					UploadedImage = UploadedImage,
				};

				// Save the certificate entity to your database or perform any necessary actions
				await _context.CreateCertificateAsync(certificate);

				return RedirectToPage("/Certificates/Index");
			}
			catch (Exception ex)
			{
				// Log the error or handle it appropriately
				ErrorMessage = "An error occurred while processing your request.";
				return Page();
			}
		}
	}
}
