﻿using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos;
using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Back.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CandidateController : Controller
	{
		private readonly CertificationDbContext _context;

		public CandidateController(CertificationDbContext context)
		{
			_context = context;
		}

		// get all candidates
		[HttpGet]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<IEnumerable<CandidateDto>>> GetAllCandidatesAsync()
		{
			var result = await _context.Candidates.Include(x => x.User)
				.Select(x => CandidateDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}

		// get candidate by id
		[HttpGet("{id}")]
		public async Task<ActionResult<CandidateDto>> GetCandidateById(int id)
		{
			var candidate = await _context.Candidates.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (candidate == null)
			{
				return NotFound();
			}

			return Ok(CandidateDto.FromEntity(candidate));
		}
		// get candidate by id
		[HttpGet("Number/{candidateNumber}")]
		public async Task<ActionResult<CandidateDto>> GetCandidateByNUmber(Guid candidateNumber)
		{
			var candidate = await _context.Candidates.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Number == candidateNumber);

			if (candidate == null)
			{
				return NotFound();
			}

			return Ok(CandidateDto.FromEntity(candidate));
		}

		// get candidate by user id
		[HttpGet("user/{userId}")]
		public async Task<ActionResult<CandidateDto>> GetCandidateByUserId(int userId)
		{
			var candidate = await _context.Candidates.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.User.Id == userId);

			if (candidate == null)
			{
				return NotFound();
			}

			return Ok(CandidateDto.FromEntity(candidate));
		}


		// create candidate
		[HttpPost]
		//[Authorize(Roles = "admin,candidate")]
		public async Task<ActionResult<CandidateDto>> CreateCandidate(CandidateDto candidateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var passwordHasher = new PasswordHasher<User>();
			var encryptedPassword = passwordHasher.HashPassword(new User(), candidateDto.Password);
			var user = new User()
			{
				FirstName = candidateDto.FirstName,
				LastName = candidateDto.LastName,
				Email = candidateDto.Email,
				Phone = candidateDto.Phone ?? "",
				Address = candidateDto.Address,
				Password = encryptedPassword,
				Role = "candidate",
				CreatedAt = DateTime.Now
			};

			var candidate = new Candidate()
			{
				Number = Guid.NewGuid(),
				BirthDate = candidateDto.BirthDate,
				User = user
			};

			_context.Candidates.Add(candidate);
			await _context.SaveChangesAsync();

			return Ok(candidateDto);
		}

		// update candidate
		[HttpPut("{id}")]
		public async Task<ActionResult<CandidateDto>> UpdateCandidate(int id, CandidateDto candidateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var candidate = await _context.Candidates.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);

			var passwordHasher = new PasswordHasher<User>();
			var encryptedPassword = passwordHasher.HashPassword(new User(), candidateDto.Password);

			if (candidate == null)
			{
				return NotFound();
			}

			candidate.User.FirstName = candidateDto.FirstName;
			candidate.User.LastName = candidateDto.LastName;
			candidate.User.Email = candidateDto.Email;
			candidate.User.Password = encryptedPassword;
			candidate.User.Phone = candidateDto.Phone;
			candidate.User.Address = candidateDto.Address;
			candidate.BirthDate = candidateDto.BirthDate;

			await _context.SaveChangesAsync();

			return Ok(CandidateDto.FromEntity(candidate));
		}

		// delete candidate
		[HttpDelete("{id}")]
		public async Task<ActionResult<CandidateDto>> DeleteCandidate(int id)
		{
			var candidate = await _context.Candidates.Include(x => x.User)
				.FirstOrDefaultAsync(x => x.Id == id);


			if (candidate == null)
			{
				return NotFound();
			}

			_context.Candidates.Remove(candidate);

			//delete user as well
			_context.Users.Remove(candidate.User);
			await _context.SaveChangesAsync();

			return Ok(CandidateDto.FromEntity(candidate));
		}
	}
}
