using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{

    [Authorize(Roles = "admin,qualityAssurance,marker")]
    public class IndexModel : PageModel
    {
        private readonly MarkerService _markerService;

        public IndexModel(MarkerService markerService)
        {
			_markerService = markerService;
        }

        public IEnumerable<MarkerDto> Markers { get; set; }

        public async Task OnGetAsync()
        {
			Markers = await _markerService.GetAllMarkersAsync();
        }

        //get marker by id
        public async Task<IActionResult> OnGetMarkerByIdAsync(int id)
        {
            var marker = await _markerService.GetMarkerByIdAsync(id);
            if (marker == null)
            {
                return NotFound();
            }
            return new JsonResult(marker);
        }
    }
}
