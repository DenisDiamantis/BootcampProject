using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Entities.Exam
{
    public class ExamAnswer
    {
        public int Id { get; set; }
        public int ExamQuestionId { get; set; } // Foreign key to the ExamQuestion
        public string Text { get; set; } // The answer text
        public bool IsCorrect { get; set; } // Indicates if this answer is correct
        public ExamQuestion Question { get; set; }
    }
}
