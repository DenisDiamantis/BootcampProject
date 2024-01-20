using FinalProject.Data.Entities;

namespace FinalProject.Data.Dtos.CertificateDtos
{
	public class UserCertificateViewDto
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public string Grade { get; set; }

		public DateTime Created { get; set; }

		public string Status { get; set; }

		public int UserId { get; set; }
		public int CertificateId { get; set; }
		public User User { get; set; }

		public Certificate Certificate { get; set; }

		public static UserCertificateViewDto FromEntity(UserCertificate userCertificate)
		{
			return new UserCertificateViewDto
			{
				Id = userCertificate.Id,
				Title = userCertificate.Title,
				Description = userCertificate.Description,
                ImageUrl = userCertificate.ImageUrl,
				Grade = userCertificate.Grade,
				Created = userCertificate.Created,
				Status = userCertificate.Status,
				User = userCertificate.User,
				Certificate = userCertificate.Certificate,
				UserId = userCertificate.UserId,
				CertificateId = userCertificate.CertificateId
			};
		}
	}
}
