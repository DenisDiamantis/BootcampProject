using FinalProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.MarkerDtos
{
	public class MarkerDto
	{
		public int Id { get; set; }
		public Guid Number { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public DateTime BirthDate { get; set; }
		public string Password { get; set; }

		public static MarkerDto FromEntity(Marker marker)
		{
			return new MarkerDto
			{
				Id = marker.Id,
				Number = marker.Number,
				FirstName = marker.User.FirstName,
				LastName = marker.User.LastName,
				Phone = marker.User.Phone,
				Email = marker.User.Email,
				Address = marker.User.Address,
				BirthDate = marker.BirthDate
			};
		}
	}
}
