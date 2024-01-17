using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Back.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserCertificateController : Controller
	{
		private readonly CertificationDbContext _context;
		private readonly IHttpContextAccessor _contextAccessor;

		public UserCertificateController(CertificationDbContext context, IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
			_context = context;
		}

		//UserCertificate CRUD

		[HttpPost("{examId}")]
		//[Authorize(Roles = "admin")]
		public async Task<ActionResult> CreateUserCertificate(int examId)
		{

			var exam = await _context.Exam.FirstOrDefaultAsync(x => x.Id == examId);
			var candidate = await _context.Candidates.Include(x => x.User).FirstOrDefaultAsync(c => c.Number == exam.CandidateNumber);
			var certificate = _context.Certificates.FirstOrDefault(ce => ce.Id == exam.CertificateId);

			var userCertificate = UserCertificate.ToEntity(candidate.User, certificate);
			userCertificate.Grade = exam.CandidateScore.ToString() + "%";
			_context.UserCertificates.Add(userCertificate);
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet]
		//[Authorize(Roles = "admin")]
		public async Task<ActionResult<IEnumerable<UserCertificateViewDto>>> GetAllCertificates()
		{

			var result = await _context.UserCertificates
				.Select(x => UserCertificateViewDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}
		[HttpGet("{userId}")]

		public async Task<ActionResult<IEnumerable<UserCertificateViewDto>>> GetAllCertificatesById(int userId)
		{
			var result = await _context.UserCertificates.Where(uc => uc.UserId == userId)
				.Select(x => UserCertificateViewDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}


		//[HttpPut("{id}")]
		//[Authorize(Roles = "admin")]
		//public async Task<ActionResult<UserCertificateViewDto>> UpdateUserCertificateDto(int id, string status)
		//{
		//	var userCertificate = await _context.UserCertificates.FirstOrDefaultAsync(x => x.Id == id);

		//	if (userCertificate == null)
		//	{
		//		return NotFound();
		//	}

		//	userCertificate.UpdateStatus(status);

		//	await _context.SaveChangesAsync();

		//	return Ok(UserCertificateViewDto.FromEntity(userCertificate));
		//}


	}
}
