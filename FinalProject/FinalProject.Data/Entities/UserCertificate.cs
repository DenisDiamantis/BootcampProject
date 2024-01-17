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
		public int UserId { get; set; }
		public int CertificateId { get; set; }

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
				Status = "Issued",
				User = user,
				Certificate = certificate,
				Grade = "TBD"

			};
		}

		public UserCertificate UpdateGrade(string grade)
		{
			this.Grade = grade;

			return this;
		}
	}
}
