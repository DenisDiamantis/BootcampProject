using Microsoft.AspNetCore.Http;

namespace FinalProject.Data.Dtos.CertificateDtos
{
    public class CertificateCreateDto
    {
        public string Title { get; set; }
		public string Description { get; set; }

		public double Cost { get; set; }

		public IFormFile UploadedImage { get; set; }
	}
}
