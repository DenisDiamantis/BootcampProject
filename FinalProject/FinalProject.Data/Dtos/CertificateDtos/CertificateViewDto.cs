using FinalProject.Data.Entities;

namespace FinalProject.Data.Dtos.CertificateDtos
{
    public class CertificateViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }

		public double Cost { get; set; }

		public string Photo { get; set; }


		public static CertificateViewDto FromEntity(Certificate certificate)
        {
            return new CertificateViewDto
            {
                Id = certificate.Id,
                Title = certificate.Title,
				Description = certificate.Description,
				Cost = certificate.Cost,
				Photo = certificate.Photo,
			};
        }
    }
}
