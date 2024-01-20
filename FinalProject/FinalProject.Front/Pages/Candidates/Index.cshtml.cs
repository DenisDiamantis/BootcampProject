using FinalProject.Data.Dtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Candidates
{


	public class IndexModel : PageModel
	{
		private readonly CandidateService _candidateService;

		public IndexModel(CandidateService candidateService)
		{
			_candidateService = candidateService;
		}

		public IEnumerable<CandidateDto> Candidates { get; set; }

		public async Task OnGetAsync()
		{
			Candidates = await _candidateService.GetAllCandidatesAsync();
		}

		//get candidate by id
		public async Task<IActionResult> OnGetCandidateByIdAsync(int id)
		{
			var candidate = await _candidateService.GetCandidateByIdAsync(id);
			if (candidate == null)
			{
				return NotFound();
			}
			return new JsonResult(candidate);
		}
	}
}
