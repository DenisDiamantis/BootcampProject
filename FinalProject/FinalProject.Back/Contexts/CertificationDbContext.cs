using FinalProject.Data.Entities;
using FinalProject.Data.Entities.Exam;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Back.Contexts
{
	public class CertificationDbContext : DbContext
	{
		public CertificationDbContext(DbContextOptions<CertificationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					Email = "neilos@neko.com",
					FirstName = "Neilos",
					LastName = "Kotsiopoulos",
					Phone = "123",
					Address = "Nea Smirni",
					Role = "admin",
					Password = @"AQAAAAIAAYagAAAAEPyPqDdsy1Dm0/9ha5foebLh3wvlwuycOtrqQVXdq66uW14eYgIKOaypZHfkANnKCQ==",
					CreatedAt = DateTime.Now
				}
				);
			base.OnModelCreating(modelBuilder);
		}

		public async Task<List<UserCertificate>> GetAllCertificatesByUserIdAsync(int userId)
		{
			return await UserCertificates
				.Where(x => x.User.Id == userId)
				.ToListAsync();
		}

		public DbSet<Certificate> Certificates { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Candidate> Candidates { get; set; }

		public DbSet<UserCertificate> UserCertificates { get; set; }

		public DbSet<ExamTemplate> ExamTemplate { get; set; }
		public DbSet<Exam> Exam { get; set; }
		public DbSet<QuestionTemplate> QuestionTemplate { get; set; }

		public DbSet<Answer> Answer { get; set; }


	}
}
