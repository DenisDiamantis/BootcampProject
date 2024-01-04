using FinalProject.Data.Dtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Candidates
{
    public class DetailsModel : PageModel
    {
        private readonly CandidateService _candidateService;

        public CandidateDto Candidate { get; set; }

        public DetailsModel(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            Candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (Candidate == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
