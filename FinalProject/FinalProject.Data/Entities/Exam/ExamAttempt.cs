using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Entities.Exam
{
    public class ExamAttempt
    {
        public int Id { get; set; }
        public int CandidateId { get; set; } // Foreign key to the Candidate entity

        public int CertificateId { get; set; } // Foreign key to the Certificate entity
        public DateTime AttemptDate { get; set; }
        public int Score { get; set; } // The candidate's score for this attempt
        public bool Passed { get; set; } // Indicates if the candidate passed the exam
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; }
    }
}
