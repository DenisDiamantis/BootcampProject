using FinalProject.Data.Dtos.AcountDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
	public class AssignMarkerModel : PageModel
	{
		private readonly AccountService _accountService;

		private readonly ExamService _context;

		public AssignMarkerModel(AccountService accountService, ExamService context)
		{
			_accountService = accountService;
			_context = context;
		}

		public IEnumerable<UserProfileDto> Markers { get; set; }
		public IEnumerable<ExamDto> Exams { get; set; }

		public async Task OnGetAsync(int examId)
		{
			Markers = await _accountService.GetMarkersAsync();
			Exams = await _context.GetAllExams();
			ViewData["examId"] = examId;
		}

		public async Task OnPostAsync(int id)
		{

		}
	}
}
