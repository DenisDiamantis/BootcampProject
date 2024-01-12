using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Services;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Data.Entities
{
    public class Certificate
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        [NotMapped]
        public IFormFile UploadedImage { get; set; }
		public string ImageUrl { get; set; }

		public static Certificate ToEntity(CertificateCreateDto certificateCreateDto, IFileStorageService fileStorageService)
		{
			// Assuming IFileStorageService has a method like SaveFile(IFormFile file) that returns the saved file path.
			string imagePath = null;

			if (certificateCreateDto.UploadedImage != null)
			{
				imagePath = fileStorageService.SaveFile(certificateCreateDto.UploadedImage);
			}

			return new Certificate
			{
				Title = certificateCreateDto.Title,
				Description = certificateCreateDto.Description,
				Cost = certificateCreateDto.Cost,
				ImageUrl = imagePath,
				UploadedImage = certificateCreateDto.UploadedImage,
			};
		}
	}
}
