using FinalProject.Data.Entities.Exam;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class ExamDto
    {
        public int Id { get; set; }
        public Guid CandidateNumber { get; set; } // Foreign key to the Candidate entity
        public int CertificateId { get; set; }
        public int ExamTemplateId { get; set; } // Foreign key to the Certificate entity
        public DateTime ExamDate { get; set; }
        public int CandidateScore { get; set; }
        public bool Marked { get; set; }
        public bool Result { get; set; } // Indicates if the candidate passed the exam



        public static ExamDto FromEntity(Exam exam)
        {
            return new ExamDto
            {
                Id = exam.Id,
                CandidateNumber = exam.CandidateNumber,
                ExamTemplateId = exam.ExamTemplateId,
                ExamDate = exam.ExamDate,
                CandidateScore = exam.CandidateScore,
                Result = exam.Result,
                Marked = exam.Marked,
                CertificateId = exam.CertificateId

            };
        }

    }
}
