using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Front.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FinalProject.Front.Pages.Exams
{
    public class CandidateExamModel : PageModel
    {
        private readonly ExamService _context;
        private readonly CertificateService _certificateHelper;


        public CandidateExamModel(ExamService examService, CertificateService certificateService)
        {
            _context = examService;
            _certificateHelper = certificateService;
        }

        public IEnumerable<ExamDto> Exams { get; set; }
        public IEnumerable<CertificateViewDto> Certificates { get; set; }
        public IEnumerable<ExamTemplateDto> examTemplates { get; set; }


        public async Task OnGetAsync()
        {

            Exams = await _context.GetAvailableExams(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value));
            Certificates = await _certificateHelper.GetAllCertificatesAsync();
            examTemplates = await _context.GetAllExamTemplates();
        }
    }
}
