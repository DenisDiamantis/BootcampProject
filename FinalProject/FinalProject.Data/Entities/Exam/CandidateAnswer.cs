using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Entities.Exam
{
    public class CandidateAnswer
    {
        public int Id { get; set; }
        public int ExamAttemptId { get; set; } // Foreign key to the ExamAttempt
        public int ExamQuestionId { get; set; } // Foreign key to the ExamQuestion
        public int ExamAnswerId { get; set; } // Foreign key to the ExamAnswer
        public bool IsSelected { get; set; }   
    }
}
