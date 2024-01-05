using FinalProject.Data.Entities.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class ExamQuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int CertificateId { get; set; }
        public List<ExamAnswerDto> Answers { get; set; }

        public static ExamQuestionDto FromEntity(ExamQuestion examQuestion)
        {
            return new ExamQuestionDto
            {
                Id = examQuestion.Id,
                Text = examQuestion.Text,
                CertificateId = examQuestion.CertificateId,
                Answers = examQuestion.Answers.Select(ExamAnswerDto.FromEntity).ToList()
            };
        }

        public static ExamQuestion ToEntity(ExamQuestionDto examQuestionDto)
        {
            return new ExamQuestion
            {
                Id = examQuestionDto.Id,
                Text = examQuestionDto.Text,
                CertificateId = examQuestionDto.CertificateId,
                Answers = examQuestionDto.Answers.Select(ExamAnswerDto.ToEntity).ToList()
            };
        }
    }
}
