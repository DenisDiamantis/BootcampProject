using FinalProject.Data.Entities.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class ExamAnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public bool IsCorrect { get; set; } 

        public static ExamAnswerDto FromEntity(ExamAnswer examAnswer)
        {
            return new ExamAnswerDto
            {
                Id = examAnswer.Id,
                Text = examAnswer.Text,
                IsCorrect = examAnswer.IsCorrect
            };
        }

        public static ExamAnswer ToEntity(ExamAnswerDto examAnswerDto)
        {
            return new ExamAnswer
            {
                Id = examAnswerDto.Id,
                Text = examAnswerDto.Text,
                IsCorrect = examAnswerDto.IsCorrect
            };
        }
    }
}
