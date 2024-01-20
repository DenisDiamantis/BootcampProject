using FinalProject.Data.Entities;

namespace FinalProject.Data.Dtos.AcountDtos
{
	public class UserProfileDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = "";

		public string LastName { get; set; } = "";

		public string Email { get; set; } = "";

		public string? Phone { get; set; } = "";

		public string Address { get; set; } = "";

		public string? Role { get; set; } = "";
		public string Password { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public static UserProfileDto FromEntity(User user)
		{
			return new UserProfileDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Phone = user.Phone,
				Address = user.Address,
				Role = user.Role,
				CreatedAt = user.CreatedAt,
				Password = user.Password

			};
		}


	}


}
