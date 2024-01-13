using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Front.Pages.Exams
{
    public class ExamModel : PageModel
    {
        private readonly ExamService _context;
        private readonly CertificateService _certificateHelper;
        private static int Number;
        private static int ExamTemplateId;

        public ExamModel(ExamService context, CertificateService certificateHelper)
        {
            _context = context;
            _certificateHelper = certificateHelper;
        }


        [BindProperty]
        public List<QuestionTemplateDto> Questions { get; set; }
        public async Task OnGetAsync(int id)
        {
            Questions = (await _context.GetQuestions(id)).ToList();

        }
    }
}
