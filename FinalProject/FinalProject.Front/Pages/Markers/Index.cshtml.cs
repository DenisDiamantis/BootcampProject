using FinalProject.Data.Dtos.AcountDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
	public class MarkersModel : PageModel
	{
		private readonly AccountService _accountService;

		private readonly ExamService _context;

		public MarkersModel(AccountService accountService, ExamService context)
		{
			_accountService = accountService;
			_context = context;
		}

		public IEnumerable<UserProfileDto> Markers { get; set; }
		public IEnumerable<ExamDto> Exams { get; set; }

		public async Task OnGetAsync()
		{
			Markers = await _accountService.GetMarkersAsync();
			Exams = await _context.GetAllExams();

		}
	}
}
