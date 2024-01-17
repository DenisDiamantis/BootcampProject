using FinalProject.Data.Dtos.AcountDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
	//[Authorize(Roles = "admin,candidate")]


	public class CreateModel : PageModel
	{
		private readonly AccountService _accountService;

		public CreateModel(AccountService accountService)
		{
			_accountService = accountService;
		}

		[BindProperty]
		public UserProfileDto Marker { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{

			await _accountService.CreateAccount(Marker);
			//if user is not logged in, redirect to login else redirect to index
			return Redirect("./Index");
			//if user is logged in as admin, redirect to index
			//return RedirectToPage("/Candidates/Index");

		}
	}
}
