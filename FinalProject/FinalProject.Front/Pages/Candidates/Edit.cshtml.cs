using FinalProject.Data.Dtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Candidates
{
	[Authorize(Roles = "admin")]
	public class EditModel : PageModel
	{
		private readonly CandidateService _candidateService;

		public EditModel(CandidateService candidateService)
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
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _candidateService.UpdateCandidateAsync(Candidate);
			return RedirectToPage("/Candidates/Index");
		}

	}
}
