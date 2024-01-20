using FinalProject.Data.Dtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Candidates
{
	//[Authorize(Roles = "admin,candidate")]


	public class CreateModel : PageModel
	{
		private readonly CandidateService _candidateService;

		public CreateModel(CandidateService candidateService)
		{
			_candidateService = candidateService;
		}

		[BindProperty]
		public CandidateDto Candidate { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _candidateService.CreateCandidateAsync(Candidate);
			//if user is not logged in, redirect to login else redirect to index
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToPage("/Candidates/Index");
			}
			else
			{
				return RedirectToPage("/Account/Login");
			}



			//if user is logged in as admin, redirect to index
			//return RedirectToPage("/Candidates/Index");

		}
	}
}
