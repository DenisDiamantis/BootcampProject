using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Markers
{
	public class EvaluationModel : PageModel
	{
		private readonly ExamService _context;

		public EvaluationModel(ExamService context)
		{
			_context = context;
		}
		[BindProperty]
		public IEnumerable<QuestionTemplateDto> Questions { get; set; }
		public IEnumerable<AnswerDto> Answers { get; set; }

		public async Task OnGetAsync(int examId)
		{
			Questions = await _context.GetQuestions(examId);
			Answers = await _context.getAnswers(examId);
			ViewData["examId"] = examId;
		}
	}
}
