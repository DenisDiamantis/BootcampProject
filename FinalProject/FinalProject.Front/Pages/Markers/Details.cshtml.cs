using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
    public class DetailsModel : PageModel
    {
        private readonly MarkerService _markerService;

        public MarkerDto Marker { get; set; }

        public DetailsModel(MarkerService markerService)
        {
			_markerService = markerService;
        }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            Marker = await _markerService.GetMarkerByIdAsync(id);
            if (Marker == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
