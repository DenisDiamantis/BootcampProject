using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

		[HttpPost()]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<AsyncVoidMethodBuilder>> CreateUserCertificate(UserCertificateCreateDto createDto)
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			var certificate = await _context.Certificates.FirstOrDefaultAsync(ce => ce.Id == createDto.Id);
			int userId = Int32.Parse(userIdClaim?.Value!);
			var user = _context.Users.FirstOrDefault(u => u.Id == userId);

			var userCertificate = UserCertificate.ToEntity(user, certificate);

			_context.UserCertificates.Add(userCertificate);
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpGet]
		[Authorize(Roles = "candidate,admin,marker")]
		public async Task<ActionResult<IEnumerable<UserCertificateViewDto>>> GetAllCertificatesByUserId()
		{
			var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
			int userId = Int32.Parse(userIdClaim?.Value);

			var certificates = await _context.GetAllCertificatesByUserIdAsync(userId);

			var result = await _context.UserCertificates
				.Select(x => UserCertificateViewDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}


		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<UserCertificateViewDto>> UpdateUserCertificateDto(int id, string status)
		{
			var userCertificate = await _context.UserCertificates.FirstOrDefaultAsync(x => x.Id == id);

			if (userCertificate == null)
			{
				return NotFound();
			}

			userCertificate.UpdateStatus(status);

			await _context.SaveChangesAsync();

			return Ok(UserCertificateViewDto.FromEntity(userCertificate));
		}


	}
}
