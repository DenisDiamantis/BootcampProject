using FinalProject.Data.Entities.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class CandidateAnswerDto
    {
        public int Id { get; set; }
        public int ExamQuestionId { get; set; }
        public int ExamAnswerId { get; set; }
        public bool IsSelected { get; set; }


        public static CandidateAnswerDto FromEntity(CandidateAnswer candidateAnswer)
        {
            return new CandidateAnswerDto
            {
                Id = candidateAnswer.Id,
                ExamQuestionId = candidateAnswer.ExamQuestionId,
                ExamAnswerId = candidateAnswer.ExamAnswerId,
                IsSelected = candidateAnswer.IsSelected
            };
        }
    }
}
