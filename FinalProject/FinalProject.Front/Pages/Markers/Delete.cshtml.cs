using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{

	[Authorize(Roles = "admin")]
	public class DeleteModel : PageModel
	{
		private readonly MarkerService _markerService;


		public DeleteModel(MarkerService markerService, AccountService accountService)
		{
			_markerService = markerService;

		}

		[BindProperty]
		public MarkerDto Marker { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Marker = await _markerService.GetMarkerByIdAsync(id);
			if (Marker == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			await _markerService.DeleteMarkerAsync(Marker.Id);
			return RedirectToPage("/Markers/Index");
		}
	}
}
