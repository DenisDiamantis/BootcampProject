using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Entities.Exam
{
    public class ExamQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; } // The actual question text

        public int CertificateId { get; set; } // Foreign key to the Certificate
        public ICollection<ExamAnswer> Answers { get; set; }

    }
}
