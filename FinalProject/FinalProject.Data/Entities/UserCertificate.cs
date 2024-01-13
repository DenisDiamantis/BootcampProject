using FinalProject.Data.Dtos.AcountDtos;
using FinalProject.Data.Dtos.CertificateDtos;

namespace FinalProject.Data.Entities
{
	public class UserCertificate
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public string Description { get; set; }

		public string Photo { get; set; }

		public string Grade { get; set; }

		public DateTime Created { get; set; } 

		public string Status { get; set; }

		public User User { get; set; }

		public Certificate Certificate { get; set; }


		public static UserCertificate ToEntity(User user, Certificate certificate)
		{
			return new UserCertificate
			{
				Title = certificate.Title,
				Description = certificate.Description,
				Photo = certificate.Photo,
				Created = DateTime.Now,
				Status = "Puchased",
				User = user,
				Certificate = certificate,
				Grade = "TBD"

			};
		}

		public  UserCertificate UpdateStatus(string status) 
		{ 
			this.Status = status;

			return this;
		}
	}
}
