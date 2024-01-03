using FinalProject.Data.Dtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Candidates
{
	[Authorize(Roles = "admin")]
	public class DeleteModel : PageModel
	{
		private readonly CandidateService _candidateService;


		public DeleteModel(CandidateService candidateService, AccountService accountService)
		{
			_candidateService = candidateService;

		}

		[BindProperty]
		public CandidateDto Candidate { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Candidate = await _candidateService.GetCandidateByIdAsync(id);
			if (Candidate == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			await _candidateService.DeleteCandidateAsync(Candidate.Id);
			return RedirectToPage("/Candidates/Index");
		}
	}
}
