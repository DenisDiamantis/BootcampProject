using FinalProject.Data.Entities;

namespace FinalProject.Data.Dtos
{
	public class CandidateDto
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
		public int UserId { get; set; }

		public static CandidateDto FromEntity(Candidate candidate)
		{
			return new CandidateDto
			{
				Id = candidate.Id,
				Number = candidate.Number,
				FirstName = candidate.User.FirstName,
				LastName = candidate.User.LastName,
				Phone = candidate.User.Phone,
				Email = candidate.User.Email,
				Address = candidate.User.Address,
				BirthDate = candidate.BirthDate,
				UserId = candidate.UserId

			};
		}
	}
}
