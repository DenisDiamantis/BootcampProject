using FinalProject.Data.Dtos;
using FinalProject.Data.Dtos.MarkerDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
	//[Authorize(Roles = "admin,marker")]


	public class CreateModel : PageModel
	{
		private readonly MarkerService _markerService;

		public CreateModel(MarkerService markerService)
		{
			_markerService = markerService;
		}

		[BindProperty]
		public MarkerDto Marker { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _markerService.CreateMarkerAsync(Marker);
			//if user is not logged in, redirect to login else redirect to index
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToPage("/Markers/Index");
			}
			else
			{
				return RedirectToPage("/Account/Login");
			}



			//if user is logged in as admin, redirect to index
			//return RedirectToPage("/Markers/Index");

		}
	}
}
