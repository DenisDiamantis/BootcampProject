using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
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
        private readonly MarkerService _markerService;
        public CandidateDto Candidate { get; set; }
        public MarkerDto Marker { get; set; }

        public DetailsModel(CandidateService candidateService, MarkerService markerService)
        {
            _candidateService = candidateService;
            _markerService = markerService;
        }

        //public DetailsModel(MarkerService markerService)
        //{
        //    _markerService = markerService;
        //}

        public async Task OnGetAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                NotFound(); // Handle the case where no user is logged in
            }
            //convert string to int
            //var userIdInt = int.Parse(userId);

            if (int.TryParse(userId, out var userIdInt))
            {
                if (userIdInt == 1)
                {
                     Page();
                }
				//var candidateTask = _candidateService.GetCandidateByUserIdAsync(userIdInt);
				//var markerTask = _markerService.GetMarkerByUserIdAsync(userIdInt);

				//await Task.WhenAll(candidateTask, markerTask);

				//Candidate = candidateTask.Result;
				//Marker = markerTask.Result;	
				if (User.IsInRole("Candidate")) 
                {
					Candidate = await _candidateService.GetCandidateByUserIdAsync(userIdInt);
					if (Candidate == null)
					{
						NotFound(); // Handle the case where no candidate is associated with the user
					}
				}
                if (User.IsInRole("marker"))
                {
					Marker = await _markerService.GetMarkerByUserIdAsync(userIdInt); 
                    if (Marker == null)
					{
						NotFound(); // Handle the case where no marker is associated with the user
					}
				}
                		
			}
            else
            {
                BadRequest("Invalid user ID");
            }
            Page();
        }
    }
}
