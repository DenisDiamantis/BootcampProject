using FinalProject.Data.Dtos.ExamDtos;

namespace FinalProject.Data.Entities.Exam
{
	public class QuestionTemplate
	{
		public int Id { get; set; }
		public int ExamTemplateId { get; set; }
		public required string QuestionText { get; set; }
		public required string AnswerA { get; set; }
		public required string AnswerB { get; set; }
		public required string AnswerC { get; set; }
		public required string AnswerD { get; set; }
		public required string CorrectAnwser { get; set; }

		public static QuestionTemplate ToEntity(QuestionTemplateDto question)
		{
			return new QuestionTemplate
			{
				Id = question.Id,
				ExamTemplateId = question.ExamTemplateId,
				QuestionText = question.QuestionText,
				AnswerA = question.AnswerA,
				AnswerB = question.AnswerB,
				AnswerC = question.AnswerC,
				AnswerD = question.AnswerD,
				CorrectAnwser = question.CorrectAnwser
			};
		}
	}
}