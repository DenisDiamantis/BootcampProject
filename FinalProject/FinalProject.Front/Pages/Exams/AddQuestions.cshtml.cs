using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{

    public class AddQuestionsModel : PageModel
    {
        private readonly ExamService _context;
        private readonly CertificateService _certificateHelper;
        private static int Number;
        private static int ExamTemplateId;

        public AddQuestionsModel(ExamService context, CertificateService certificateHelper)
        {
            _context = context;
            _certificateHelper = certificateHelper;
        }

        public static int NumberOfQuestions() { return Number; }

        [BindProperty]
        public IEnumerable<QuestionTemplateDto> Questions { get; set; }
        [BindProperty]
        public QuestionTemplateDto Question { get; set; }
        public async Task OnGetAsync(int numberOfQuetions, int examTemplateId)
        {
            Number = numberOfQuetions;
            ExamTemplateId = examTemplateId;
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.AddQuestionAsync(Question, ExamTemplateId);

            if (Number == 1)
            {
                return RedirectToPage("/Exams/Index");
            }
            return RedirectToPage("/Exams/AddQuestions", new { numberOfQuetions = --Number, examTemplateId = ExamTemplateId });
        }

    }
}
