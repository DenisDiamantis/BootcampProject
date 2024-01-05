using FinalProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.CertificateDtos
{
	public class UserCertificateViewDto
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

		public static UserCertificateViewDto FromEntity(UserCertificate userCertificate)
		{
			return new UserCertificateViewDto
			{
				Id = userCertificate.Id,
				Title = userCertificate.Title,
				Description = userCertificate.Description,
				Photo = userCertificate.Photo,
				Grade = userCertificate.Grade,
				Created = userCertificate.Created,
				Status = userCertificate.Status,
				User = userCertificate.User,
				Certificate = userCertificate.Certificate
			};
		}
	}
}
