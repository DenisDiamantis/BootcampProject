using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinalProject.Back.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MarkerController : Controller
	{
		private readonly CertificationDbContext _context;

		public MarkerController(CertificationDbContext context)
		{
			_context = context;
		}

		// get all markers
		[HttpGet]
		//[Authorize(Roles = "admin")]
		public async Task<ActionResult<IEnumerable<MarkerDto>>> GetAllCertificates()
		{
			var result = await _context.Markers.Include(x => x.User)
				.Select(x => MarkerDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}

		// get marker by id
		[HttpGet("{id}")]
		public async Task<ActionResult<MarkerDto>> GetMarkerById(int id)
		{
			var marker = await _context.Markers.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (marker == null)
			{
				return NotFound();
			}

			return Ok(MarkerDto.FromEntity(marker));
		}

		// get marker by user id
		[HttpGet("user/{userId}")]
		public async Task<ActionResult<MarkerDto>> GetMarkerByUserId(int userId)
		{
			var marker = await _context.Markers.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.User.Id == userId);

			if (marker == null)
			{
				return NotFound();
			}

			return Ok(MarkerDto.FromEntity(marker));
		}


		// create marker
		[HttpPost]
		//[Authorize(Roles = "admin,marker")]
		public async Task<ActionResult<MarkerDto>> CreateMarker(MarkerDto markerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var passwordHasher = new PasswordHasher<User>();
			var encryptedPassword = passwordHasher.HashPassword(new User(), markerDto.Password);
			var user = new User()
			{
				FirstName = markerDto.FirstName,
				LastName = markerDto.LastName,
				Email = markerDto.Email,
				Phone = markerDto.Phone ?? "",
				Address = markerDto.Address,
				Password = encryptedPassword,
				Role = "marker",
				CreatedAt = DateTime.Now
			};

			var marker = new Marker()
			{
				Number = Guid.NewGuid(),
				BirthDate = markerDto.BirthDate,
				User = user
			};

			_context.Markers.Add(marker);
			await _context.SaveChangesAsync();

			return Ok(markerDto);
		}

		// update marker
		[HttpPut("{id}")]
		public async Task<ActionResult<MarkerDto>> UpdateMarker(int id, MarkerDto markerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var marker = await _context.Markers.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);

			var passwordHasher = new PasswordHasher<User>();
			var encryptedPassword = passwordHasher.HashPassword(new User(), markerDto.Password);

			if (marker == null)
			{
				return NotFound();
			}

			marker.User.FirstName = markerDto.FirstName;
			marker.User.LastName = markerDto.LastName;
			marker.User.Email = markerDto.Email;
			marker.User.Password = encryptedPassword;
			marker.User.Phone = markerDto.Phone;
			marker.User.Address = markerDto.Address;
			marker.BirthDate = markerDto.BirthDate;

			await _context.SaveChangesAsync();

			return Ok(MarkerDto.FromEntity(marker));
		}

		// delete marker
		[HttpDelete("{id}")]
		public async Task<ActionResult<MarkerDto>> DeleteMarker(int id)
		{
			var marker = await _context.Markers.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);


			if (marker == null)
			{
				return NotFound();
			}

			_context.Markers.Remove(marker);

			//delete user as well
			_context.Users.Remove(marker.User);
			await _context.SaveChangesAsync();

			return Ok(MarkerDto.FromEntity(marker));
		}
	}
}
