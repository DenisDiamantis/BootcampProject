using FinalProject.Data.Dtos;
using FinalProject.Data.Entities;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;



namespace FinalProject.Front.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private readonly CandidateService _candidateService;
        public CandidateDto Candidate { get; set; }

        public DetailsModel(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                   return NotFound(); // Handle the case where no user is logged in
            }
            //convert string to int
            var userIdInt = int.Parse(userId);   
            
            if(userIdInt == 1)
            {
                return Page();
            }
           
            Candidate = await _candidateService.GetCandidateByUserIdAsync(userIdInt);
            if (Candidate == null)
            {
                return NotFound(); // Handle the case where no candidate is associated with the user
            }

            return Page();
        }
    }
}
