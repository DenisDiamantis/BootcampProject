using FinalProject.Data.Entities.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class ExamAttemptDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }

        public int CertificateId { get; set; }
        public DateTime AttemptDate { get; set; }
        public int Score { get; set; }
        // You might include the candidate's selected answers here
        public List<CandidateAnswerDto> CandidateAnswers { get; set; }

        public bool Passed { get; set; }



        public static ExamAttemptDto FromEntity(ExamAttempt examAttempt)
        {
            return new ExamAttemptDto
            {
                Id = examAttempt.Id,
                CandidateId = examAttempt.CandidateId,
                AttemptDate = examAttempt.AttemptDate,
                Score = examAttempt.Score,
                CandidateAnswers = examAttempt.CandidateAnswers.Select(CandidateAnswerDto.FromEntity).ToList(),
                Passed = examAttempt.Passed,
                CertificateId = examAttempt.CertificateId
            };
        }
    }
}
